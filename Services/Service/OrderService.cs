using Services.Controller;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service {

    public class OrderService : IOrderService {

        public int AddOrder(Order order) {
            OrderController controller = new OrderController();
            return controller.AddOrder(order);
        }

        public bool AddSalesLineItemToOrder(List<SalesLineItem> sli) {
            OrderController controller = new OrderController();
            return controller.AddSalesLineItemToOrder(sli);
        }

        public void ChangeOrderToPaid(Order order) {
            OrderController controller = new OrderController();
            controller.ChangeOrderToPaid(order);
        }

        public List<Order> GetAllOrders() {
            OrderController controller = new OrderController();
            return controller.GetAllOrders();
        }

        public Order GetOrder(int id) {
            OrderController controller = new OrderController();
            return controller.GetOrder(id);
        }
    }
}