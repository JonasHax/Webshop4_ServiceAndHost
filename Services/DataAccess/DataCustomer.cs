﻿using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Services.DataAccess
{
    public class DataCustomer
    {
        private readonly string _connectionString;

        // Connectionstring for the database.
        public DataCustomer()
        {
            _connectionString = @"data source = .\SQLEXPRESS; Integrated Security=true; Database=Webshop";
        }

           
        public bool SaveCustomer(Customer aCustomer)
        {

            bool result = false;
            string insertString;
            insertString = "INSERT INTO Customer(firstName, lastName, street, houseNo, zipCode, email, phoneNumber, hashedPassword) VALUES (@FirstName, @LastName, @Street, @HouseNo, @ZipCode, @Email, @PhoneNumber, @HashedPassword)";

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
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
                    SqlParameter hashedPasswordParam = new SqlParameter("@HashedPassword", aCustomer.Password);
                    createCommand.Parameters.Add(hashedPasswordParam);

                    con.Open();
                    int rows = createCommand.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                result = false;
            }

            return result;
        }

        public Customer getCustomer(int id)
        {
            Customer foundCustomer = null;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand getCommand = con.CreateCommand())
                    {
                        getCommand.CommandText = "SELECT * FROM Customer WHERE customerID = @Id";
                        getCommand.Parameters.AddWithValue("Id", id);

                        SqlDataReader reader = getCommand.ExecuteReader();
                        while (reader.Read())
                        {
                            foundCustomer = new Customer()
                            {
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
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
            }
            
            return foundCustomer;
        }
    }
}
