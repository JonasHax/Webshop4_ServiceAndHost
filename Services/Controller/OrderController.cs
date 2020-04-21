﻿using Services.DataAccess;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Controller {
    public class OrderController {
        public int AddOrder(Order order) {
            DataOrder orderAccess = new DataOrder();
            return orderAccess.AddOrder(order);
        }

        public bool AddSalesLineItemToOrder(SalesLineItem sli) {
            DataOrder orderAccess = new DataOrder();
            return orderAccess.AddSalesLineItemToOrder(sli);
        }
    }
}