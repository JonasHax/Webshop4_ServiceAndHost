using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.DataAccess;
using Services.Model;
using Services.Service;
using System.Data.Common;
using System.Data.SqlClient;

namespace Test
{
    [TestClass]
    public class Test
    {
        private string _connectionString;
        

        [TestMethod]
        public void CustomerService()
        {
            //Arrange
           _connectionString = @"data source = .\SQLEXPRESS; Integrated Security=true; Database=Webshop";

            SqlConnection con = new SqlConnection();

            con.ConnectionString = _connectionString;
            con.Open();


            Assert.IsNotNull(con);






        }
    }
}
