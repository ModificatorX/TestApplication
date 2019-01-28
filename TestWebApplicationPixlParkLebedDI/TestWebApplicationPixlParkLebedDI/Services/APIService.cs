using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestWebApplicationPixlParkLebedDI.Models;
using System.Security.Cryptography;
using System.Text;
using TestWebApplicationPixlParkLebedDI.Models.Orders;

namespace TestWebApplicationPixlParkLebedDI.Services
{
    public class APIService: IAPIService
    {
        public class TokenRequest
        {
            public string RequestToken { get; set; }
            public int Expires { get; set; }
            public bool Success { get; set; }
        }

        public class TokenAccess
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
            public int Expires { get; set; }
            public bool Success { get; set; }
        }

        

        static HttpClient client = new HttpClient();
        static string reqtoken;
        static string acctoken;
        static string username= "38cd79b5f2b2486d86f562e3c43034f8";
        static string password;
        static string privateKey= "8e49ff607b1f46e1a5e8f6ad5d312a80";
        private List<Order> orders;

        public APIService()
        {
            client.BaseAddress = new Uri("http://api.pixlpark.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<Order>> getOrders()
        {
            try
            {
                await getRequestToken();
                await getAccessToken();
                orders = await getOrdersList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                orders = null;
            }
            
            return orders;
        }

        static async Task getRequestToken()
        {
            TokenRequest req = null;
            HttpResponseMessage response = await client.GetAsync(
               "oauth/requesttoken");
            if (response.IsSuccessStatusCode)
            {
                req = await response.Content.ReadAsAsync<TokenRequest>();
                reqtoken = req.RequestToken;
            }

        }

        static async Task getAccessToken()
        {
            string toHash = reqtoken + privateKey;
            using (SHA1Cng sha = new SHA1Cng())
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(toHash));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                   
                    sb.Append(b.ToString("x2"));
                }
                password = sb.ToString();
            } ;
            TokenAccess acc = null;
            HttpResponseMessage response = await client.GetAsync(
               $"oauth/accesstoken?oauth_token={reqtoken}&grant_type=api&username={username}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                acc = await response.Content.ReadAsAsync<TokenAccess>();
                acctoken = acc.AccessToken;
            }
        }

        static async Task<List<Order>> getOrdersList()
        {

            List<Order> orders = new List<Order>();
            HttpResponseMessage response = await client.GetAsync(
               $"orders?oauth_token={acctoken}");
            if (response.IsSuccessStatusCode)
            {
                
                    OrdersList result = await response.Content.ReadAsAsync<OrdersList>();
                return result.Result.ToList<Order>();
            }
            return null;
        }
    }
}