using Services.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Services.DataAccess
{

    public class DataOrder
    {
        private readonly string _connectionString;

        // Opretter forbindelse til databasen
        public DataOrder()
        {
            _connectionString = @"data source = .\SQLEXPRESS; Integrated Security=true; Database=Webshop2";
        }

        // Tilføjer en ordre til databasen samt giver den et order id som kan vises i desktopklienten
        public int AddOrder(Order order)
        {
            int generatedOrderId;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand addCommand = connection.CreateCommand())
                {
                    addCommand.CommandText = "INSERT INTO SalesOrder OUTPUT INSERTED.orderID VALUES (@Date, @State, @CustID, NULL, NULL)";
                    addCommand.Parameters.AddWithValue("Date", order.Date);
                    addCommand.Parameters.AddWithValue("State", order.Status);
                    addCommand.Parameters.AddWithValue("CustID", order.CustomerId);

                    generatedOrderId = (int)addCommand.ExecuteScalar();
                }
            }
            return generatedOrderId;
        }

        //Metode der tilføjer salgslinjen til ordren, her håndteres der samtidighed ved hjælp af repeatable read.
        public bool AddSalesLineItemToOrder(List<SalesLineItem> sli)
        {
            bool result = false;

            // Laver en transaction option som sætter isolationsniveauet til repeatableread
            TransactionOptions to = new TransactionOptions();
            to.IsolationLevel = IsolationLevel.RepeatableRead;

            int deadlockRetries = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    while (deadlockRetries < 3)
                    {
                        foreach (SalesLineItem lineItem in sli)
                        {
                            try
                            {
                                // SQL kommando til at få fat i underprodukter.
                                using (SqlCommand getStockCommand = connection.CreateCommand())
                                {
                                    getStockCommand.CommandText = "SELECT stock FROM ProductVersion WHERE productID = @ProdID AND sizeCode = @SizeCode AND colorCode = @ColorCode";
                                    getStockCommand.Parameters.AddWithValue("ProdID", lineItem.Product.StyleNumber);
                                    getStockCommand.Parameters.AddWithValue("SizeCode", lineItem.ProductVersion.SizeCode);
                                    getStockCommand.Parameters.AddWithValue("ColorCode", lineItem.ProductVersion.ColorCode);

                                    var stock = (int)getStockCommand.ExecuteScalar();

                                    // tjek om nok på lager
                                    if (stock < lineItem.amount)
                                    {
                                        throw new Exception("Not enough in stock of product: " + lineItem.Product.Name);
                                    }
                                    else
                                    {
                                        // indsæt saleslineitem
                                        using (SqlCommand insertSalesLineCommand = connection.CreateCommand())
                                        {
                                            insertSalesLineCommand.CommandText = "INSERT INTO SalesLineItem VALUES (@Amount, @Price, @OrderID, @ProductID, @SizeCode, @ColorCode)";
                                            insertSalesLineCommand.Parameters.AddWithValue("Amount", lineItem.amount);
                                            insertSalesLineCommand.Parameters.AddWithValue("Price", lineItem.Price);
                                            insertSalesLineCommand.Parameters.AddWithValue("OrderID", lineItem.Order.OrderId);
                                            insertSalesLineCommand.Parameters.AddWithValue("ProductID", lineItem.Product.StyleNumber);
                                            insertSalesLineCommand.Parameters.AddWithValue("SizeCode", lineItem.ProductVersion.SizeCode);
                                            insertSalesLineCommand.Parameters.AddWithValue("ColorCode", lineItem.ProductVersion.ColorCode);
                                            // execute
                                            insertSalesLineCommand.ExecuteNonQuery();
                                        }

                                        // opdater lager
                                        using (SqlCommand updateStockCommand = connection.CreateCommand())
                                        {
                                            updateStockCommand.CommandText = "UPDATE ProductVersion SET stock = stock - @Amount WHERE productID = @ProdID AND sizeCode = @SizeCode AND colorCode = @ColorCode";
                                            updateStockCommand.Parameters.AddWithValue("Amount", lineItem.amount);
                                            updateStockCommand.Parameters.AddWithValue("ProdID", lineItem.Product.StyleNumber);
                                            updateStockCommand.Parameters.AddWithValue("SizeCode", lineItem.ProductVersion.SizeCode);
                                            updateStockCommand.Parameters.AddWithValue("ColorCode", lineItem.ProductVersion.ColorCode);
                                            // execute
                                            updateStockCommand.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            catch (SqlException)
                            {
                                // Fejll kode 1205 fra Db = Deadlock
                                //if (ex.Number == 1205)
                                deadlockRetries++;
                            }
                        }
                    }
                }
                scope.Complete(); // end transaction
                result = true;
            }

            return result;
        }

        // Metode der ændre status på ordren.
        public void ChangeOrderToPaid(Order order)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE SalesOrder SET orderStatus = 'True' WHERE orderID = @OrderID";
                    cmd.Parameters.AddWithValue("OrderID", order.OrderId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Metode til at retunere en order med et givet OrderID
        public Order GetOrder(int id)
        {
           Order FoundOrder = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand getCommand = connection.CreateCommand())
                {
                    getCommand.CommandText = "SELECT * FROM SalesOrder WHERE OrderID = @ID";
                    getCommand.Parameters.AddWithValue("ID", id);

                    SqlDataReader reader = getCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        FoundOrder = new Order()
                        {
                            OrderId = reader.GetInt32(reader.GetOrdinal("orderID")),
                            CustomerId = reader.GetInt32(reader.GetOrdinal("customerID")),
                            Date = reader.GetDateTime(reader.GetOrdinal("purchaseDate")),
                            Status = reader.GetBoolean(reader.GetOrdinal("orderStatus")),
                         
                        };
                    }
                }
            }

            return FoundOrder;
        }
    }
}