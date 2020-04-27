using Services.DataAccess;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Controller {

    public class ProductController {

        public Product GetProduct(int id) {
            // går til database lag
            DataProduct dp = new DataProduct();
            return dp.GetProduct(id);
        }

        public List<Product> GetAllProducts() {
            DataProduct dp = new DataProduct();
            return dp.GetAllProducts();
        }

        public bool InsertProduct(Product productToInsert) {
            DataProduct dp = new DataProduct();
            return dp.InsertProduct(productToInsert);
        }

        public List<string> GetAllCategories() {
            DataProduct dp = new DataProduct();
            return dp.GetAllCategories();
        }

        //public List<string> GetCategory(int id)
        //{
        //    DataProduct dp = new DataProduct();
        //    return dp.GetCategory(id);
        //}

        public List<string> GetAllColors() {
            DataProduct dp = new DataProduct();
            return dp.GetAllColors();
        }

        public List<string> GetAllSizes() {
            DataProduct dp = new DataProduct();
            return dp.GetAllSizes();
        }

        public bool InsertProductVersion(ProductVersion prodVerToInsert, int styleNumber) {
            DataProduct dp = new DataProduct();
            return dp.InsertProductVersion(prodVerToInsert, styleNumber);
        }

        public bool InsertProductCategoryRelation(int styleNumber, string category) {
            DataProduct dp = new DataProduct();
            return dp.InsertProductCategoryRelation(styleNumber, category);
        }

        public bool DeleteProductTESTINGPURPOSES() {
            DataProduct dp = new DataProduct();
            return dp.DeleteProductTESTINGPURPOSES();
        }

        public bool DeleteProduct(int stylenumber) {
            DataProduct dp = new DataProduct();
            return dp.DeleteProduct(stylenumber);
        }

        public bool DeleteProductVersion(int styleNumber, string sizeCode, string colorCode) {
            DataProduct dp = new DataProduct();
            return dp.DeleteProductVersion(styleNumber, sizeCode, colorCode);
        }

        public bool UpdateProductVersion(int styleNumber, string sizeCode, string colorCode, int newStock) {
            DataProduct dp = new DataProduct();
            return dp.UpdateProductVersion(styleNumber, sizeCode, colorCode, newStock);
        }

        public bool UpdateProduct(Product productToUpdate) {
            DataProduct dp = new DataProduct();
            return dp.UpdateProduct(productToUpdate);
        }
    }
}