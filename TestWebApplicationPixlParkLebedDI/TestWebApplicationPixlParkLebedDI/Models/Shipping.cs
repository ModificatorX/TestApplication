using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApplicationPixlParkLebedDI.Models
{
    public class Shipping
    {
        //public string OrderId { get; set; }
        //public Order Order { get; set; }
        public int    Id { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ShippingType { get; set; }
    }
}