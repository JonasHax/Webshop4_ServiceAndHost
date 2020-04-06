using Services.Model;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services {

    public class TestClass {

        private static void Main(string[] args) {
            Product product;
            //IProductService service = new ProductService();
            ProductService service = new ProductService();
            //product = service.GetProduct(2, "l", "beige");
            //Product product2 = service.GetProduct(3, "m", "blue");

            //Console.WriteLine(product);
            //Console.WriteLine("-----------");
            //Console.WriteLine(product2);

            product = service.GetProduct(3);

            Console.WriteLine(product);
            foreach (ProductVersion productVersion in product.ProductVersions) {
                Console.WriteLine(productVersion);
                //Console.WriteLine(productVersion.Product);
            }

            Console.Read();
        }
    }
}