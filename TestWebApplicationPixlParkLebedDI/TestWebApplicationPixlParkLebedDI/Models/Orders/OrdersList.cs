using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApplicationPixlParkLebedDI.Models.Orders
{
    public class OrdersList
    {
        public string ApiVersion { get; set; }
        public Order[] Result { get; set; }
    }
}