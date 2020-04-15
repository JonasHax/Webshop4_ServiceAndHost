using Services.Model;
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

        // Connectionstring for your database, you might need to change it to your own specific address.
        public DataCustomer()
        {
            _connectionString = @"data source = .\SQLEXPRESS; Integrated Security=true; Database=Webshop4";
        }

        public int SaveCustomer(Customer aCustomer)
        {
            int insertedId = -1;

            string insertString;

            insertString = "INSERT INTO Customer(firstName, lastName, street, houseNo, zipCode, email, phoneNumber, hashedPassword) VALUES (@FirstName, @LastName, @Street, @HouseNo, @ZipCode, @Email, @PhoneNumber, @HashedPassword)";

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
                // Execute save
                //insertedId = (int)createCommand.ExecuteScalar();
                createCommand.ExecuteScalar();
            }

            return insertedId;
        }
        
    }
}
