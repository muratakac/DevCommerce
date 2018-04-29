using DevCommerce.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using DevCommerce.WebUI.Extensions;

namespace DevCommerce.WebUI.Controllers
{
    public class ClientBaseController : Controller
    {
        public ClientBaseController()
        {

        }

        private static string Token;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientBaseController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        static ClientBaseController()
        {
            SetToken();
        }

        //TODO => Buradaki değerler config dosyasından alınacak
        public static void SetToken()
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
                            response = client.PostAsync(requestUri, stringContent).Result;
                        }
                        break;
                    case RequestType.GET:
                    default:
                        response = client.GetAsync(requestUri).Result;
                        break;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    SetToken();
                    return ServiceGetData(requestUri, requestType, postParameters, requeiredToken);
                }
                else
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}

