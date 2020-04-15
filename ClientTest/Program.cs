using ClientTest.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Services.Model;

//using proxy = ClientTest.ProductService;

namespace ClientTest {

    public class Program {

        public static void Main(string[] args) {
            using (ProductService.ProductServiceClient prox = new ProductService.ProductServiceClient()) {
                try {
                    Product product = prox.GetProduct(3);

                    if (product != null) {
                        Console.WriteLine(product);
                        foreach (var item in product.ProductVersions) {
                            Console.WriteLine(item);
                        }
                    }

                    
                } catch (CommunicationException e) {
                    Console.WriteLine(e.StackTrace);
                }
            }

            Console.Read();
        }
    }
}