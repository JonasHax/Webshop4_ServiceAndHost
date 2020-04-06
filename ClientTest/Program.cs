using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

//using proxy = ClientTest.ProductService;

namespace ClientTest {

    public class Program {

        public static void Main(string[] args) {
            using (ProductService.ProductServiceClient prox = new ProductService.ProductServiceClient()) {
                try {
                    ProductService.Product product = prox.GetProduct(3);

                    if (product != null) {
                        Console.WriteLine(product);
                    }
                } catch (CommunicationException e) {
                    Console.WriteLine(e.StackTrace);
                }
            }

            Console.Read();
        }
    }
}