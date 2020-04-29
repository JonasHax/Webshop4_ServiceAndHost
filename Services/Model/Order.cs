using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model {
    // Orderklassen med datacontract header viser at data kan exchanges til service og at det er serializebale 
    [DataContract]
    public class Order {
    // Getters and setters for making an Order
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public bool Status { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public List<SalesLineItem> SalesLineItems { get; set; }

        public Order() {
            SalesLineItems = new List<SalesLineItem>();
        }
    }   
}
