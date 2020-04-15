using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HostServices
{

    internal class Host
    {

        private static void Main(string[] args)
        {
            using (ServiceHost productServiceHost = new ServiceHost(typeof(ProductService)))
            {
                productServiceHost.Open();
                
                if (productServiceHost.State == CommunicationState.Opened)
                {
                    DisplayHostInfo(productServiceHost);
                    // Keep the service running until the Enter key is pressed
                    Console.WriteLine("The service is ready.");
                }
                else
                {
                    Console.WriteLine("The service is not ready.");
                    Console.WriteLine("Press the Enter key to terminate.");
                }


                using (ServiceHost customerServiceHost = new ServiceHost(typeof(CustomerService)))
                {
                    customerServiceHost.Open();
                    
                    if (customerServiceHost.State == CommunicationState.Opened)
                    {
                        DisplayHostInfo(customerServiceHost);
                        // Keep the service running until the Enter key is pressed
                        Console.WriteLine("The service is ready.");
                        Console.WriteLine("Press the Enter key to terminate service.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("The service is not ready.");
                        Console.WriteLine("Press the Enter key to terminate.");
                        
                    }
                }
                Console.ReadLine();
            }

        }
        


        //    using (ServiceHost productServiceHost = new ServiceHost(typeof(ProductService)))
        //    {
        //        // Open the host and start listening for incoming calls
        //        productServiceHost.Open();
        //        //employeeServiceHost.Open();

        //        if (productServiceHost.State == CommunicationState.Opened)
        //        {
        //            DisplayHostInfo(productServiceHost);
        //            //DisplayHostInfo(employeeServiceHost);
        //            // Keep the service running until the Enter key is pressed
        //            Console.WriteLine("The service is ready.");
        //            Console.WriteLine("Press the Enter key to terminate service.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("The service is not ready.");
        //            Console.WriteLine("Press the Enter key to terminate.");
        //        }
        //        Console.ReadLine();
        //    }
        //}
        //    using (ServiceHost customerServiceHost = new ServiceHost(typeof(CustomerService)))
        //    {
        //        // Open the host and start listening for incoming calls
        //        customerServiceHost.Open();
        //        //employeeServiceHost.Open();

        //        if (customerServiceHost.State == CommunicationState.Opened)
        //        {
        //            DisplayHostInfo(customerServiceHost);
        //             //DisplayHostInfo(employeeServiceHost);
        //            // Keep the service running until the Enter key is pressed
        //            Console.WriteLine("The service is ready.");
        //            Console.WriteLine("Press the Enter key to terminate service.");
        //        }
        //        else
        //        {
        //            Console.WriteLine("The service is not ready.");
        //            Console.WriteLine("Press the Enter key to terminate.");
        //        }
        //        Console.ReadLine();
        //    }

        //    Console.ReadLine();
        //}


        private static void DisplayHostInfo(ServiceHost host)
        {
            Console.WriteLine();
            Console.WriteLine("*-- Host Info --*");

            foreach (System.ServiceModel.Description.ServiceEndpoint se in host.Description.Endpoints)
            {
                Console.WriteLine($"Address: {se.Address}");
                Console.WriteLine($"Binding: {se.Binding}");
                Console.WriteLine($"Contract: {se.Contract}");
            }
            Console.WriteLine("*---------------*");
        }
    }
}