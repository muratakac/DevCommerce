using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DevCommerce.Core.CrossCuttingConcerns
{
    public class DevCommerceProxy
    {
        private readonly RequestDelegate _next;

        public DevCommerceProxy(RequestDelegate next)
        {
            //https://blogs.msdn.microsoft.com/dotnet/2016/09/19/custom-asp-net-core-middleware-example/
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/extensibility-third-party-container?view=aspnetcore-2.0
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Call the next delegate/middleware in the pipeline
            Begin(context.Request);

            await this._next(context);

            End(context.Response);

        }

        public void Begin(HttpRequest request) { }
        public void End(HttpResponse response) { }
    }
}
