using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestWebApplicationPixlParkLebedDI.Models;
using TestWebApplicationPixlParkLebedDI.Models.Orders;
using TestWebApplicationPixlParkLebedDI.Services;
namespace TestWebApplicationPixlParkLebedDI.Controllers
{
    public class OrdersController : Controller
    {
        static readonly IAPIService _apiservice=new APIService();
       /* public OrdersController(
            IAPIService apiservice
            )
        {
            _apiservice = apiservice;
        }*/
        public async Task<ActionResult> Index()
        {
            return View();
        }
        public async Task<ActionResult> OrdersList()
        {
            var orders= await _apiservice.getOrders();
            if (orders == null) return View("Index");
            return View("OrdersList", new OrdersList
            {
                Result = orders.ToArray()
            });
        }
    }
}