using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace VII
{
    class Customer
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public double Purchase { get; set; }

        public Customer()
        {
        }

        public Customer(int id, string name, string phone, double purchase)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Purchase = purchase;
        }

        public override string ToString()
        {
            return $"Customer [Name={Name}, Id={Id}, Phone={Phone}, Purchase={Purchase}]";
        }
    }
}

