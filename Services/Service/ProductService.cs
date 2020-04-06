using Services.Controller;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service {

    public class ProductService : IProductService {

        public Product GetProduct(int id) {
            ProductController pc = new ProductController();
            return pc.GetProduct(id);
        }
    }
}