using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DataAccess;
using Services.Model;

namespace Services.Controller {

    public class CustomerController {

        public bool InsertCustomer(Customer customerToInsert) {
            DataCustomer customerDb = new DataCustomer();
            return customerDb.SaveCustomer(customerToInsert);
        }

        public Customer GetCustomer(int id) {
            DataCustomer customerDb = new DataCustomer();
            return customerDb.getCustomer(id);
        }

        public Customer CustomerLogin(string email, string password) {
            DataCustomer customerDB = new DataCustomer();
            return customerDB.CustomerLogin(email, password);
        }
    }
}