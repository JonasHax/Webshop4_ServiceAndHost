using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model {

    [DataContract]
    public class ProductVersion {

        [DataMember]
        public Product Product { get; set; }

        [DataMember]
        public int Stock { get; set; }

        [DataMember]
        public string SizeCode { get; set; }

        [DataMember]
        public string ColorCode { get; set; }

        public ProductVersion() {
        }

        public override string ToString() {
            return $"Size: {SizeCode}, Color: {ColorCode}, stock: {Stock}";
        }
    }
}