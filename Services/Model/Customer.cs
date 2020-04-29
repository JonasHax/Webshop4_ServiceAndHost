using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{

    // Kundeklassen med datacontract header viser at det er serializable og kan exchange data med servicen
    [DataContract]
    public class Customer
    {
        // Getters og setters til information om kunden
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Street { get; set; }      
        [DataMember]
        public int HouseNo { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }

        // Constructor
        public Customer(string firstName, string lastName,  string custStreet, int custNo, string zipCode, string phoneNumber, string email, string password)
        {
            
        }

        public Customer()
        {
        }
    }
}
