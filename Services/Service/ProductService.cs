using Services.Controller;
using Services.DataAccess;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service {

    public class ProductService : IProductService {

        public List<string> GetAllColors() {
            ProductController pc = new ProductController();
            return pc.GetAllColors();
        }

        public List<string> GetAllSizes() {
            ProductController pc = new ProductController();
            return pc.GetAllSizes();
        }

        public List<Product> GetAllProducts() {
            ProductController pc = new ProductController();
            return pc.GetAllProducts();
        }

        public int GetANumber(int number) {
            return number + 2;
        }

        public Product GetProduct(int id) {
            ProductController pc = new ProductController();
            return pc.GetProduct(id);
        }

        public bool InsertProduct(Product productToInsert) {
            ProductController pc = new ProductController();
            return pc.InsertProduct(productToInsert);
        }

        public List<string> GetAllCategories() {
            ProductController pc = new ProductController();
            return pc.GetAllCategories();
        }

        public bool InsertProductVersion(ProductVersion prodVerToInsert, int styleNumber) {
            ProductController pc = new ProductController();
            return pc.InsertProductVersion(prodVerToInsert, styleNumber);
        }

        public bool InsertProductCategoryRelation(int styleNumber, string category)
        {
            ProductController pc = new ProductController();
            return pc.InsertProductCategoryRelation(styleNumber, category);
        }

        //public List<ProductVersion> GetProductVersionsByProductID(int id) {
        //    DataProduct dp = new DataProduct();
        //    return dp.GetProductVersionsByProductID(id);
        //}
    }
}