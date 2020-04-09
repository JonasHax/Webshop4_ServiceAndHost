using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Services.Model
{
    [DataContract]
    public class Customer
    {
        //[DataMember]
        //public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string CustStreet { get; set; }      
        [DataMember]
        public int CustNo { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }

        public Customer(string firstName, string lastName,  string custStreet, int custNo, string zipCode, string phoneNumber, string email, string password)
        {
            //Id = Id;
        }
    }
}
