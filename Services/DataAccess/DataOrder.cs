using Services.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataAccess {
    public class DataOrder {
        private readonly string _connectionString;

        public DataOrder() {
            _connectionString = @"data source = CHEDZ-DESKTOP\SQLEXPRESS; Integrated Security=true; Database=Webshop";
        }

        public int AddOrder(Order order) {
            int generatedOrderId;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand addCommand = connection.CreateCommand()) {
                    addCommand.CommandText = "INSERT INTO SalesOrder OUTPUT INSERTED.orderID VALUES (@Date, @State, @CustID, NULL, NULL)";
                    addCommand.Parameters.AddWithValue("Date", order.Date);
                    addCommand.Parameters.AddWithValue("State", order.Status);
                    addCommand.Parameters.AddWithValue("CustID", order.CustomerId);

                    generatedOrderId = (int)addCommand.ExecuteScalar();
                }
            }
            return generatedOrderId;
        }

        public bool AddSalesLineItemToOrder(SalesLineItem sli) {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "INSERT INTO SalesLineItem VALUES (@Amount, @Price, @OrderID, @ProductID, @SizeCode, @ColorCode)";
                    command.Parameters.AddWithValue("Amount", sli.amount);
                    command.Parameters.AddWithValue("Price", sli.Price);
                    command.Parameters.AddWithValue("OrderID", sli.Order.OrderId);
                    command.Parameters.AddWithValue("ProductID", sli.Product.StyleNumber);
                    command.Parameters.AddWithValue("SizeCode", sli.ProductVersion.SizeCode);
                    command.Parameters.AddWithValue("ColorCode", sli.ProductVersion.ColorCode);

                    int rows = command.ExecuteNonQuery();

                    if (rows > 0) {
                        result = true;
                    }

                }
            }
            return result;
        }
    }
}
