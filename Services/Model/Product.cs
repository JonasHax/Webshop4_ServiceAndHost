using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model {
    // Productklassen er en datacontract = Kan exchange data med service og er serializable
    [DataContract]
    public class Product {
        /// <summary>
        ///  Getters and setters til produketet.
        /// </summary>
        [DataMember]
        public int StyleNumber { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Boolean State { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<ProductVersion> ProductVersions { get; set; }

        public Product() {
            ProductVersions = new List<ProductVersion>();
        }

        public override string ToString() {
            return $"Varenummer: {StyleNumber} \nNavn: {Name}\nPris: {Price},-\nBeskrivelse: {Description}\nTilgængelig: {State}"; /*\nLager: {Stock} \nStr.: {SizeCode} \nFarve: {ColorCode}*/
        }

        //public void AddProductVersion(ProductVersion prodVer) {
        //    ProductVersions.Add(prodVer);
        //}

        public List<ProductVersion> GetProductVersions() {
            return this.ProductVersions;
        }
    }
}