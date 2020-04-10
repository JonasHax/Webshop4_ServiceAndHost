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

        // Connectionstring for your database, you might need to change it to your own specific address.
        public DataProduct() {
            //_connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            _connectionString = @"data source = CHEDZDESKTOP\SQLEXPRESS01; Integrated Security=true; Database=Webshop";
        }

        // Method to get the base product from the database.
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

            foundProduct.ProductVersions = GetProductVersionsByProductID(id).ToList();

            return foundProduct;
        }

        // Method that gets all the subproducts from the base product.
        private List<ProductVersion> GetProductVersionsByProductID(int id) {
            List<ProductVersion> list = new List<ProductVersion>();
            //Product product = GetProduct(id);

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                ProductVersion prodVersion = null;
                connection.Open();
                using (SqlCommand prodVersionCommand = connection.CreateCommand()) {
                    prodVersionCommand.CommandText = "SELECT * FROM ProductVersion WHERE productID = @ID";
                    prodVersionCommand.Parameters.AddWithValue("ID", id);

                    SqlDataReader reader = prodVersionCommand.ExecuteReader();
                    while (reader.Read()) {
                        prodVersion = new ProductVersion() {
                            Product = null,
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

        // Retrieves all the products connected with its subproducts from the database
        public List<Product> GetAllProducts() {
            List<Product> productList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand getProducts = connection.CreateCommand()) {
                    getProducts.CommandText = "SELECT * FROM Product";

                    SqlDataReader reader = getProducts.ExecuteReader();
                    while (reader.Read()) {
                        Product newProduct = new Product() {
                            StyleNumber = reader.GetInt32(reader.GetOrdinal("styleNumber")),
                            Description = reader.GetString(reader.GetOrdinal("prodDescription")),
                            Name = reader.GetString(reader.GetOrdinal("prodName")),
                            State = reader.GetBoolean(reader.GetOrdinal("prodState")),
                            Price = reader.GetDecimal(reader.GetOrdinal("price"))
                        };
                        productList.Add(newProduct);
                    }
                }
            }

            foreach (Product product in productList) {
                product.ProductVersions = GetProductVersionsByProductID(product.StyleNumber).ToList();
            }

            return productList;
        }

        public bool InsertProduct(Product productToInsert) {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand insertCommand = connection.CreateCommand()) {
                    insertCommand.CommandText = "INSERT INTO Product VALUES (@Name, @Description, @State, @Price)";
                    insertCommand.Parameters.AddWithValue("Name", productToInsert.Name);
                    insertCommand.Parameters.AddWithValue("Description", productToInsert.Description);
                    insertCommand.Parameters.AddWithValue("State", productToInsert.State);
                    insertCommand.Parameters.AddWithValue("Price", productToInsert.Price);

                    int rows = insertCommand.ExecuteNonQuery();

                    if (rows > 0) {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool InsertProductVersion(ProductVersion prodVerToInsert, int styleNumber) {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand insertCommand = connection.CreateCommand()) {
                    insertCommand.CommandText = "INSERT INTO ProductVersion VALUES (@StyleNumber, @Stock, @SizeCode, @ColorCode)";
                    insertCommand.Parameters.AddWithValue("StyleNumber", styleNumber);
                    insertCommand.Parameters.AddWithValue("Stock", prodVerToInsert.Stock);
                    insertCommand.Parameters.AddWithValue("SizeCode", prodVerToInsert.SizeCode);
                    insertCommand.Parameters.AddWithValue("ColorCode", prodVerToInsert.ColorCode);

                    int rows = insertCommand.ExecuteNonQuery();

                    if (rows > 0) {
                        result = true;
                    }
                }
            }
            return result;
        }

        public List<string> GetAllColors() {
            List<string> listOfColors = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand getColorsCommand = connection.CreateCommand()) {
                    getColorsCommand.CommandText = "SELECT * FROM ColorCode";

                    SqlDataReader reader = getColorsCommand.ExecuteReader();
                    while (reader.Read()) {
                        string color = reader.GetString(reader.GetOrdinal("color"));
                        listOfColors.Add(color);
                    }
                }
            }

            return listOfColors;
        }

        public List<string> GetAllSizes() {
            List<string> listOfSizes = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand getSizesCommand = connection.CreateCommand()) {
                    getSizesCommand.CommandText = "SELECT * FROM SizeCode";

                    SqlDataReader reader = getSizesCommand.ExecuteReader();
                    while (reader.Read()) {
                        string size = reader.GetString(reader.GetOrdinal("size"));
                        listOfSizes.Add(size);
                    }
                }
            }

            return listOfSizes;
        }

        public List<string> GetAllCategories() {
            List<string> listOfCategories = new List<string>();

            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand getCategoriesCommand = connection.CreateCommand()) {
                    getCategoriesCommand.CommandText = "SELECT * FROM Category";

                    SqlDataReader reader = getCategoriesCommand.ExecuteReader();
                    while (reader.Read()) {
                        string category = reader.GetString(reader.GetOrdinal("categoryName"));
                        listOfCategories.Add(category);
                    }
                }
            }

            return listOfCategories;
        }
    }
}