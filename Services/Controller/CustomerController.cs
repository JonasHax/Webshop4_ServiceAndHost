using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DataAccess;
using Services.Model;

namespace Services.Controller
{
    public class CustomerController
    {
        public bool InsertCustomer(Customer customerToInsert)
        
        {
            DataCustomer customerDb = new DataCustomer();
            return customerDb.SaveCustomer(customerToInsert);
        }


    }
}
