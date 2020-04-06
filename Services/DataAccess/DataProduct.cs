using Services.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataAccess {

    public class DataProduct {
        private readonly string _connectionString;

        public DataProduct() {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            //_connectionString = @"data source = CHEDZDESKTOP\SQLEXPRESS01; Integrated Security=true; Database=Webshop4";
        }

        public Product GetProduct(int id) {
            Product foundProduct = null;

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand getCommand = connection.CreateCommand()) {
                    getCommand.CommandText = "SELECT * FROM Product WHERE styleNumber = @ID";
                    getCommand.Parameters.AddWithValue("ID", id);

                    SqlDataReader reader = getCommand.ExecuteReader();
                    while (reader.Read()) {
                        foundProduct = new Product() {
                            StyleNumber = reader.GetInt32(reader.GetOrdinal("styleNumber")),
                            Description = reader.GetString(reader.GetOrdinal("prodDescription")),
                            Name = reader.GetString(reader.GetOrdinal("prodName")),
                            State = reader.GetBoolean(reader.GetOrdinal("prodState")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price"))
                        };
                    }
                }
            }

            foundProduct.ProductVersions = GetProductVersionsByProductID(foundProduct).ToList();

            return foundProduct;
        }

        private List<ProductVersion> GetProductVersionsByProductID(Product product) {
            List<ProductVersion> list = new List<ProductVersion>();

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                ProductVersion prodVersion = null;
                connection.Open();
                using (SqlCommand prodVersionCommand = connection.CreateCommand()) {
                    prodVersionCommand.CommandText = "SELECT * FROM ProductVersion WHERE productID = @ID";
                    prodVersionCommand.Parameters.AddWithValue("ID", product.StyleNumber);

                    SqlDataReader reader = prodVersionCommand.ExecuteReader();
                    while (reader.Read()) {
                        prodVersion = new ProductVersion() {
                            Product = product,
                            ColorCode = reader.GetString(reader.GetOrdinal("colorCode")),
                            SizeCode = reader.GetString(reader.GetOrdinal("sizeCode")),
                            Stock = reader.GetInt32(reader.GetOrdinal("stock"))
                        };

                        list.Add(prodVersion);
                    }
                }
            }

            return list;
        }
    }

    //private List<ProductVersion> PopulateProduct(Product prodToPopulate) {
    //    List<ProductVersion> list = new List<ProductVersion>();

    //    using (SqlConnection connection = new SqlConnection(_connectionString)) {
    //        ProductVersion prodVersion = null;
    //        connection.Open();
    //        using (SqlCommand prodVersionCommand = connection.CreateCommand()) {
    //            prodVersionCommand.CommandText = "SELECT * FROM ProductVersion WHERE productID = @ID";
    //            prodVersionCommand.Parameters.AddWithValue("ID", prodToPopulate.StyleNumber);

    //            SqlDataReader reader = prodVersionCommand.ExecuteReader();

    //            while (reader.Read()) {
    //                prodVersion = new ProductVersion() {
    //                    Product = prodToPopulate,
    //                    ColorCode = reader.GetString(reader.GetOrdinal("colorCode")),
    //                    SizeCode = reader.GetString(reader.GetOrdinal("sizeCode")),
    //                    Stock = reader.GetInt32(reader.GetOrdinal("stock"))
    //                };
    //                //Console.WriteLine(prodVersion);
    //                list.Add(prodVersion);
    //            }
    //        }
    //    }
    //    return list;
    //}
}