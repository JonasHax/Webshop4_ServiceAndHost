using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Services.Service;
using Services.Model;
using Services.DataAccess;

namespace Test {

    [TestClass]
    public class CostumerServiceTest {
        private CustomerService cs = new CustomerService();

        [TestMethod]
        public void TestInsertCustomer() {
            Customer customer = new Customer() {
                FirstName = "Bobby",
                LastName = "Olsen",
                Street = "Cykelvænget",
                HouseNo = 8,
                ZipCode = "9000",
                Email = "bolsen@teo.nu",
                PhoneNumber = "88888888",
                Password = "123456"
            };

            Assert.IsTrue(cs.AddCustomer(customer));

            // Delete the customer from the database again
            DataCustomer db = new DataCustomer();
            db.DeleteCustomerTestingPurpose();
        }

        [TestMethod]
        public void GetCustomer() {
            int customerID = 63;

            Customer customer = cs.GetCustomer(customerID);

            Assert.IsNotNull(customer.FirstName);
            Assert.IsNotNull(customer.LastName);
            Assert.IsNotNull(customer.Street);
            Assert.IsNotNull(customer.HouseNo);
            Assert.IsNotNull(customer.ZipCode);
            Assert.IsNotNull(customer.PhoneNumber);
            Assert.IsNotNull(customer.Email);
            Assert.IsNotNull(customer.Password);
        }
    }
}