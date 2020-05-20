using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Services.DataAccess {

    public class DataCustomer {
        private readonly string _connectionString;

        // Connectionstring for the database.
        public DataCustomer() {
            //_connectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            _connectionString = @"data source = .\SQLEXPRESS; Integrated Security=true; Database=Webshop3";
        }

        // Metode der skal gemme en kunde i databasen med de angivne parametre.
        public bool SaveCustomer(Customer aCustomer) {
            bool result = false;
            string insertString;

            // hash the password and get the salt
            PasswordHasher passwordHasher = new PasswordHasher();
            passwordHasher.CreateHash(aCustomer.Password);
            string hashedPassword = passwordHasher.HashedPassword;
            string salt = passwordHasher.PasswordSalt;

            insertString = "INSERT INTO Customer(firstName, lastName, street, houseNo, zipCode, email, phoneNumber, passwordSalt, hashedPassword) VALUES (@FirstName, @LastName, @Street, @HouseNo, @ZipCode, @Email, @PhoneNumber, @PasswordSalt, @HashedPassword)";
            try {
                // Opretter forbindelse til databasen og bruger stringen til at sætte informationer ind på parametrene
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con)) {
                    // Indsætter kundens informationer.
                    SqlParameter fNameParam = new SqlParameter("@FirstName", aCustomer.FirstName);
                    createCommand.Parameters.Add(fNameParam);
                    SqlParameter lNameParam = new SqlParameter("@LastName", aCustomer.LastName);
                    createCommand.Parameters.Add(lNameParam);
                    SqlParameter CustStreetParam = new SqlParameter("@Street", aCustomer.Street);
                    createCommand.Parameters.Add(CustStreetParam);
                    SqlParameter custNoParam = new SqlParameter("@HouseNo", aCustomer.HouseNo);
                    createCommand.Parameters.Add(custNoParam);
                    SqlParameter zipCodeParam = new SqlParameter("@ZipCode", aCustomer.ZipCode);
                    createCommand.Parameters.Add(zipCodeParam);
                    SqlParameter emailParam = new SqlParameter("@Email", aCustomer.Email);
                    createCommand.Parameters.Add(emailParam);
                    SqlParameter phoneNumberParam = new SqlParameter("@PhoneNumber", aCustomer.PhoneNumber);
                    createCommand.Parameters.Add(phoneNumberParam);
                    SqlParameter passwordSalt = new SqlParameter("@PasswordSalt", salt);
                    createCommand.Parameters.Add(passwordSalt);
                    SqlParameter hashedPasswordParam = new SqlParameter("@HashedPassword", hashedPassword);
                    createCommand.Parameters.Add(hashedPasswordParam);

                    con.Open();
                    // Eksekverer SQL kommandoen
                    int rows = createCommand.ExecuteNonQuery();

                    if (rows > 0) {
                        result = true;
                    }
                }
            } catch (SqlException ex) {
                //result = false;
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }

            return result;
        }

        public Customer getCustomer(int id) {
            Customer foundCustomer = null;

            try {
                using (SqlConnection con = new SqlConnection(_connectionString)) {
                    con.Open();
                    using (SqlCommand getCommand = con.CreateCommand()) {
                        getCommand.CommandText = "SELECT * FROM Customer WHERE customerID = @Id";
                        getCommand.Parameters.AddWithValue("Id", id);

                        SqlDataReader reader = getCommand.ExecuteReader();
                        while (reader.Read()) {
                            foundCustomer = new Customer() {
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Street = reader.GetString(reader.GetOrdinal("Street")),
                                HouseNo = reader.GetInt32(reader.GetOrdinal("HouseNo")),
                                ZipCode = reader.GetString(reader.GetOrdinal("ZipCode")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                Password = reader.GetString(reader.GetOrdinal("HashedPassword"))
                            };
                        }
                    }
                }
            } catch (SqlException ex) {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }

            return foundCustomer;
        }

        public Customer CustomerLogin(string email, string password) {
            Customer foundCust = null;
            string salt;

            // get the salt so the password can be hashed
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand saltCmd = con.CreateCommand()) {
                    saltCmd.CommandText = "SELECT passwordSalt FROM Customer WHERE email = @Email";
                    saltCmd.Parameters.AddWithValue("Email", email);

                    salt = (string)saltCmd.ExecuteScalar();
                }
            }

            if (salt == null) {
                return foundCust;
            }

            // hash the password
            PasswordHasher passwordHasher = new PasswordHasher();
            string hashedPassword = passwordHasher.GetHash(salt, password);

            try {
                using (SqlConnection connection = new SqlConnection(_connectionString)) {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand()) {
                        cmd.CommandText = "SELECT * FROM Customer WHERE email = @Email AND hashedPassword = @HashedPassword";
                        cmd.Parameters.AddWithValue("Email", email);
                        cmd.Parameters.AddWithValue("HashedPassword", hashedPassword);

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) {
                            foundCust = new Customer() {
                                CustomerID = reader.GetInt32(reader.GetOrdinal("customerID")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Street = reader.GetString(reader.GetOrdinal("Street")),
                                HouseNo = reader.GetInt32(reader.GetOrdinal("HouseNo")),
                                ZipCode = reader.GetString(reader.GetOrdinal("ZipCode")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"))
                            };
                        }
                    }
                }
            } catch (SqlException ex) {
                throw new Exception("Der opstod en fejl: " + ex.Message);
            }

            return foundCust;
        }

        // Metoder til at slette kunde efter test af oprettelse
        public void DeleteCustomerTestingPurpose() {
            using (SqlConnection con = new SqlConnection(_connectionString)) {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()) {
                    cmd.CommandText = "DELETE FROM Customer WHERE firstName = 'Bobby' AND lastName = 'Olsen' AND email = 'bolsen@teo.nu'";

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}