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
            _connectionString = @"data source = .\SQLEXPRESS; Integrated Security=true; Database=Webshop2";
        }

        // Method to get the base product from the database.
        public Product GetProduct(int id) {
            Product foundProduct = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand getCommand = connection.CreateCommand())
                    {
                        getCommand.CommandText = "SELECT * FROM Product WHERE styleNumber = @ID";
                        getCommand.Parameters.AddWithValue("ID", id);

                        SqlDataReader reader = getCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            foundProduct = new Product()
                            {
                                StyleNumber = reader.GetInt32(reader.GetOrdinal("styleNumber")),
                                Description = reader.GetString(reader.GetOrdinal("prodDescription")),
                                Name = reader.GetString(reader.GetOrdinal("prodName")),
                                State = reader.GetBoolean(reader.GetOrdinal("prodState")),
                                Price = reader.GetDecimal(reader.GetOrdinal("price"))
                            };
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }

            foundProduct.ProductVersions = GetProductVersionsByProductID(id).ToList();

            return foundProduct;
        }

        // Method that gets all the subproducts from the base product.
        private List<ProductVersion> GetProductVersionsByProductID(int id) {
            List<ProductVersion> list = new List<ProductVersion>();
            //Product product = GetProduct(id);
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    ProductVersion prodVersion = null;
                    connection.Open();
                    using (SqlCommand prodVersionCommand = connection.CreateCommand())
                    {
                        prodVersionCommand.CommandText = "SELECT * FROM ProductVersion WHERE productID = @ID";
                        prodVersionCommand.Parameters.AddWithValue("ID", id);

                        SqlDataReader reader = prodVersionCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            prodVersion = new ProductVersion()
                            {
                                Product = null,
                                ColorCode = reader.GetString(reader.GetOrdinal("colorCode")),
                                SizeCode = reader.GetString(reader.GetOrdinal("sizeCode")),
                                Stock = reader.GetInt32(reader.GetOrdinal("stock"))
                            };

                            list.Add(prodVersion);
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }

            return list;
        }

        // Retrieves all the products connected with its subproducts from the database
        public List<Product> GetAllProducts() {
            List<Product> productList = new List<Product>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand getProducts = connection.CreateCommand())
                    {
                        getProducts.CommandText = "SELECT * FROM Product";

                        SqlDataReader reader = getProducts.ExecuteReader();
                        while (reader.Read())
                        {
                            Product newProduct = new Product()
                            {
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
            }
            catch(SqlException ex)
            {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }

            foreach (Product product in productList) {
                product.ProductVersions = GetProductVersionsByProductID(product.StyleNumber).ToList();
            }

            return productList;
        }

        // Metode der indsætter produkt i databasen
        public bool InsertProduct(Product productToInsert) {
            bool result = false;
            try {
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
            } catch (SqlException e) {
                Console.WriteLine(e.StackTrace);
                result = false;
            }
            return result;
        }

        // Metode der indsætter underprodukter
        public bool InsertProductVersion(ProductVersion prodVerToInsert, int styleNumber) {
            bool result = false;
            try {
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
            } catch (SqlException e) {
                Console.WriteLine(e.StackTrace);
                result = false;
            }
            return result;
        }

        // Metode der henter alle farverne.
        public List<string> GetAllColors() {
            List<string> listOfColors = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand getColorsCommand = connection.CreateCommand())
                    {
                        getColorsCommand.CommandText = "SELECT * FROM ColorCode";

                        SqlDataReader reader = getColorsCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            string color = reader.GetString(reader.GetOrdinal("color"));
                            listOfColors.Add(color);
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }

            return listOfColors;
        }

        // Metode der henter alle størrelser.
        public List<string> GetAllSizes() {
            List<string> listOfSizes = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand getSizesCommand = connection.CreateCommand())
                    {
                        getSizesCommand.CommandText = "SELECT * FROM SizeCode";

                        SqlDataReader reader = getSizesCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            string size = reader.GetString(reader.GetOrdinal("size"));
                            listOfSizes.Add(size);
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }

            return listOfSizes;
        }

        // Metode der henter alle kategorier.
        public List<string> GetAllCategories() {
            List<string> listOfCategories = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand getCategoriesCommand = connection.CreateCommand())
                    {
                        getCategoriesCommand.CommandText = "SELECT * FROM Category";

                        SqlDataReader reader = getCategoriesCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            string category = reader.GetString(reader.GetOrdinal("categoryName"));
                            listOfCategories.Add(category);
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }

            return listOfCategories;
        }

        // Metode der gør det muligt at sætte en kategori på et produkt.
        public bool InsertProductCategoryRelation(int styleNumber, string category) {
            bool result = false;
            try {
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand()) {
                        command.CommandText = "INSERT INTO CategoryProduct VALUES (@ID, @Category)";
                        command.Parameters.AddWithValue("ID", styleNumber);
                        command.Parameters.AddWithValue("Category", category);

                        int rows = command.ExecuteNonQuery();

                        if (rows > 0) {
                            result = true;
                        }
                    }
                }
            } catch (SqlException e) {
                result = false;
                Console.WriteLine(e.StackTrace);
            }
            return result;
        }

        // Metoden der bruges i vores testklasse, så vi ikke hele tiden opretter et nyt produkt hver gang testen kører.
        public bool DeleteProductTESTINGPURPOSES() {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand deleteCommand = connection.CreateCommand()) {
                    deleteCommand.CommandText = "DELETE FROM Product WHERE prodName = 'Test'";

                    int rows = deleteCommand.ExecuteNonQuery();

                    if (rows > 0) {
                        result = true;
                    }
                }
            }
            return result;
        }

        // Metode til at slette et produkt
        public bool DeleteProduct(int stylenumber) {
            bool result = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM Product WHERE styleNumber = @Stylenumber";
                        cmd.Parameters.AddWithValue("Stylenumber", stylenumber);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }
            return result;
        }

        // Metode til at slette et underprodukt
        public bool DeleteProductVersion(int styleNumber, string sizeCode, string colorCode) {
            bool result = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM ProductVersion WHERE productID = @StyleNumber AND sizeCode = @SizeCode AND colorCode = @ColorCode";
                        cmd.Parameters.AddWithValue("StyleNumber", styleNumber);
                        cmd.Parameters.AddWithValue("SizeCode", sizeCode);
                        cmd.Parameters.AddWithValue("ColorCode", colorCode);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }
            return result;
        }

        // Metode der opdatere underproduktet
        public bool UpdateProductVersion(int styleNumber, string sizeCode, string colorCode, int newStock) {
            bool result = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE ProductVersion SET stock = @NewStock WHERE productID = @StyleNumber AND sizeCode = @SizeCode AND colorCode = @ColorCode";
                        cmd.Parameters.AddWithValue("StyleNumber", styleNumber);
                        cmd.Parameters.AddWithValue("SizeCode", sizeCode);
                        cmd.Parameters.AddWithValue("ColorCode", colorCode);
                        cmd.Parameters.AddWithValue("NewStock", newStock);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }
            return result;
        }

        // Metode der opdatere produktet
        public bool UpdateProduct(Product productToUpdate) {
            bool result = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE Product SET prodName = @Name, prodDescription = @Desc, prodState = @State, price = @Price WHERE styleNumber = @StyleNumber";
                        cmd.Parameters.AddWithValue("Name", productToUpdate.Name);
                        cmd.Parameters.AddWithValue("Desc", productToUpdate.Description);
                        cmd.Parameters.AddWithValue("State", productToUpdate.State);
                        cmd.Parameters.AddWithValue("Price", productToUpdate.Price);
                        cmd.Parameters.AddWithValue("StyleNumber", productToUpdate.StyleNumber);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }
            return result;
        }
    }
}