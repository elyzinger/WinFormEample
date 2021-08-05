using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiomaTestWeb.Model
{
    public class Address
    {

        public int TZ { get; set; }
        public int AddressType { get; set; }
        public string City { get; set; }
        public string St { get; set; }
        public int Number { get; set; }
        public int PostalCode { get; set; }

        public Address()
        {

        }
        public Address(int tz, int addressType, string city, string st, int number, int postalCode)
        {
            this.TZ = tz;
            this.AddressType = addressType;
            this.City = city;
            this.St = st;
            this.Number = number;
            this.PostalCode = postalCode;
        }
    }
}