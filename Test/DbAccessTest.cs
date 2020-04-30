using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.DataAccess;
using Services.Model;
using Services.Service;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Test
{
    [TestClass]
    public class DbAccessTest
    {
        private string _connectionString;
        
        // Tester om man kan forbinde til databasen. Husk at ændre data source og navnet på databasen.
        [TestMethod]
        public void CustomerService()
        {
            //Arrange
            _connectionString = @"data source = .\SQLEXPRESS; Integrated Security=true; Database=Webshop2";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = _connectionString;
            con.Open();

            
            Assert.IsNotNull(con);

        }
    }
}
