using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service {
    [ServiceContract]
    public interface IOrderService {

        [OperationContract]
        int AddOrder(Order order);

        [OperationContract]
        bool AddSalesLineItemToOrder(List<SalesLineItem> sli);

        [OperationContract]
        void ChangeOrderToPaid(Order order);

        [OperationContract]
        Order GetOrder(int id);
    }
}
