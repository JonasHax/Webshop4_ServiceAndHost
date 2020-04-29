using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model {

    // Salgslinje klassen med data contract header (IsReference = true sørger for at hvert objekt i salgslinjen bliver serialiseret)
    [DataContract(IsReference = true)]
    public class SalesLineItem {

        // Getters og setters til salgslinjen.
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
