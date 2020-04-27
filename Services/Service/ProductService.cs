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

        public bool InsertProductCategoryRelation(int styleNumber, string category) {
            ProductController pc = new ProductController();
            return pc.InsertProductCategoryRelation(styleNumber, category);
        }

        public bool DeleteProductTESTINGPURPOSES() {
            ProductController pc = new ProductController();
            return pc.DeleteProductTESTINGPURPOSES();
        }

        public bool DeleteProduct(int styleNumber) {
            ProductController pc = new ProductController();
            return pc.DeleteProduct(styleNumber);
        }

        public bool DeleteProductVersion(int styleNumber, string sizeCode, string colorCode) {
            ProductController pc = new ProductController();
            return pc.DeleteProductVersion(styleNumber, sizeCode, colorCode);
        }

        public bool UpdateProductVersion(int styleNumber, string sizeCode, string colorCode, int newStock) {
            ProductController pc = new ProductController();
            return pc.UpdateProductVersion(styleNumber, sizeCode, colorCode, newStock);
        }

        public bool UpdateProduct(Product productToUpdate) {
            ProductController pc = new ProductController();
            return pc.UpdateProduct(productToUpdate);
        }
    }
}