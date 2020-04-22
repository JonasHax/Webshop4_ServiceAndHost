using ClientTest.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Services.Model;
using Services.Service;

//using proxy = ClientTest.ProductService;

namespace ClientTest {

    public class Program {

        public static void Main(string[] args) {
            //using (ProductService.ProductServiceClient prox = new ProductService.ProductServiceClient()) {
            //    try {
            //        Product product = prox.GetProduct(3);

            //        if (product != null) {
            //            Console.WriteLine(product);
            //            foreach (var item in product.ProductVersions) {
            //                Console.WriteLine(item);
            //            }
            //        }


            //    } catch (CommunicationException e) {
            //        Console.WriteLine(e.StackTrace);
            //    }
            //}

            ProductServiceClient proxy = new ProductServiceClient();
            Product product = proxy.GetProduct(1);
            ProductVersion prodVer = product.GetProductVersions().ElementAt(1);

            Order order = new Order() {
                CustomerId = 2,
                Date = DateTime.Now,
                Status = false,
                OrderId = 11,

            };

            SalesLineItem sli = new SalesLineItem() {
                amount = 1,
                Price = 300,
                Product = product,
                ProductVersion = prodVer,
                Order = order

            };

            order.SalesLineItems.Add(sli);

            OrderService service = new OrderService();
            //service.AddOrder(order);
            service.AddSalesLineItemToOrder(order.SalesLineItems);

            Console.Read();
        }
    }
}