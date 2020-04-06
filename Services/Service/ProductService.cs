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

        public int GetANumber(int number) {
            return number + 2;
        }

        public Product GetProduct(int id) {
            ProductController pc = new ProductController();
            return pc.GetProduct(id);
        }

        public List<ProductVersion> GetProductVersionsByProductID(int id) {
            DataProduct dp = new DataProduct();
            return dp.GetProductVersionsByProductID(id);
        }
    }
}