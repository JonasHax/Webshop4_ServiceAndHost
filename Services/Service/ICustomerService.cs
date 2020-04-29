using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Services.Model;

namespace Services.Service
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        bool AddCustomer(Customer customerToAdd);

        [OperationContract]
        Customer GetCustomer(int id);
    }
}
