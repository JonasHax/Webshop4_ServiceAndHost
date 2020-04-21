using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model {
    [DataContract]
    public class SalesLineItem {
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public Order Order { get; set; }
        [DataMember]
        public Product Product { get; set; }
        [DataMember]
        public ProductVersion ProductVersion { get; set; }

        public SalesLineItem() {
        }
    }
}
