using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Model;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test {

    [TestClass]
    public class ProductServiceTest {
        ProductService ps = new ProductService();

        [TestMethod]
        public void TestGetAllProducts()
        {
            List<Product> productList = ps.GetAllProducts();
            Assert.IsTrue(productList.Count > 0);

        }

        [TestMethod]
        public void TestInsertProduct()
        {
            Product product = new Product() {
                Name = "Test",
                Description = "Test",
                State = true,
                Price = 0
            };

            Assert.IsTrue(ps.InsertProduct(product));

            // delete product from db 
            ps.DeleteProductTESTINGPURPOSES();
        }

        [TestMethod]
        public void TestGetProduct() {
            int productId = 2;

            Product product = ps.GetProduct(productId);

            Assert.IsNotNull(product.Name);
            Assert.IsNotNull(product.Description);
            Assert.IsNotNull(product.StyleNumber);
            Assert.IsNotNull(product.ProductVersions);
            Assert.IsNotNull(product.Price);

        }

        //[TestMethod]
        //public void TestInsertProductVersionToProduct() {
        //    // create a product and add it
        //    Product product = ps.GetProduct(1);

        //    int numberOfProdVer = product.ProductVersions.Count;

        //    // create prodver
        //    ProductVersion prodVer = new ProductVersion() {
        //        ColorCode = "Purple",
        //        SizeCode = "xs",
        //        Stock = 10
        //    };

        //    // insert prodver to product
        //    ps.InsertProductVersion(prodVer, product.StyleNumber);

        //    product = ps.GetProduct(1);

        //    // Assert
        //    Assert.IsTrue(product.ProductVersions.Count == (numberOfProdVer + 1));

        //}

        [TestMethod]
        public void TestGetSizes() {
            List<string> sizesList = ps.GetAllSizes();
            Assert.IsNotNull(sizesList.Count > 0);
        }

        [TestMethod]
        public void TestGetColors() {
            List<string> colorList = ps.GetAllColors();
            Assert.IsNotNull(colorList.Count > 0);
        }

        [TestMethod]
        public void TestGetCategories() {
            List<string> categoryList = ps.GetAllCategories();
            Assert.IsNotNull(categoryList.Count > 0);
        }

    }
}
