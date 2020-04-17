using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Model;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test {

    [TestClass]
    class ProductServiceTest {
        ProductService ps = new ProductService();

        [TestMethod]
        public void TestGetAllProducts()
        {
            List<Product> productList = ps.GetAllProducts();
            Assert.IsTrue(productList.Count > 0);
            Assert.IsNotNull(productList);
        }
    }
}
