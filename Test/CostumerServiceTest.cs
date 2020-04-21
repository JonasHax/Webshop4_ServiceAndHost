using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Services.Service;
using Services.Model;

namespace Test
{
    [TestClass]
    public class CostumerServiceTest
    {
        CustomerService cs = new CustomerService();

        [TestMethod]
        public void TestInsertCustomer()
        {

            Customer customer = new Customer()
            {
                FirstName = "Bobby",
                LastName = "Olsen",
                Street = "Cykelvænget",
                HouseNo = 8,
                ZipCode = "8450",
                Email = "bolsen@teo.nu",
                PhoneNumber = "88888888",
                Password = "123456"
            };


            Assert.IsTrue(cs.AddCustomer(customer));
            
            

        }
    }
}
