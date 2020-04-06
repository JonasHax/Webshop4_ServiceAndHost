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
    }
}