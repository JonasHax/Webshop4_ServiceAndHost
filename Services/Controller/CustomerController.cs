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
        public int InsertCustomer(Customer customerToInsert)
        {
            int idInsertedCustomer;

            DataCustomer customerDb = new DataCustomer();
            idInsertedCustomer = customerDb.SaveCustomer(customerToInsert);

            return idInsertedCustomer;
        }
    }
}
