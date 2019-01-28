using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using TestWebApplicationPixlParkLebedDI.Models;

namespace TestWebApplicationPixlParkLebedDI.Services
{
    public interface IAPIService
    {
         Task<List<Order>> getOrders();
    }
}