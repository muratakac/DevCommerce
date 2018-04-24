using DevCommerce.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DevCommerce.WebUI.Controllers
{
    public class ClientBaseController : Controller
    {
        private readonly static string Token;
        public ClientBaseController()
        {
            //TODO => Buradaki değerler config dosyasından alınacak
            //string stringData = ServiceGetData("/api/Account/GetToken", RequestType, "{\"CompanyName\":\"CodeDev\", \"ProjectName\":\"Commerce\", \"TokenKey\":\"Admin\",\"TokenValue\":\"Admin123\"}").Result;
            //Token = stringData.TrimStart('"').TrimEnd('"');
        }

        static ClientBaseController()
        {
            string stringData = ServiceGetData("/api/Account/GetToken", RequestType.POST, "{\"CompanyName\":\"CodeDev\", \"ProjectName\":\"Commerce\", \"TokenKey\":\"Admin\",\"TokenValue\":\"Admin123\"}");
            Token = stringData.TrimStart('"').TrimEnd('"');
        }

        public static string ServiceGetData(string requestUri, RequestType requestType, string postParameters = "", bool requeiredToken = false)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = null;

                client.BaseAddress = new Uri("http://localhost:57443");

                if (requeiredToken)
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Token}");
                }

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                switch (requestType)
                {
                    case RequestType.POST:
                        using (var stringContent = new StringContent(postParameters, System.Text.Encoding.UTF8, "application/json"))
                        {
                            response = client.PostAsync("/api/Account/GetToken", stringContent).Result;
                        }
                        break;
                    case RequestType.GET:
                    default:
                        response = client.GetAsync(requestUri).Result;
                        break;
                }
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}