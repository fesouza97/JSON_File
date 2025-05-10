using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_File.Model
{
    internal class Name
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
    }

    internal class Address
    {
        public string city { get; set; }
        public string street { get; set; }
        public int number { get; set; }
        public string zipcode { get; set; }
    }

    internal class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Name name { get; set; }
        public Address address { get; set; }
        public string phone { get; set; }
    }
}
