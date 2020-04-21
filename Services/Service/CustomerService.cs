using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Controller;
using Services.Model;

namespace Services.Service
{
    public class CustomerService : ICustomerService
    {
        public bool AddCustomer(Customer customerToAdd)
        
        {
            CustomerController ctrlCustomer = new CustomerController();
            return ctrlCustomer.InsertCustomer(customerToAdd);
        }

        
    }
}
