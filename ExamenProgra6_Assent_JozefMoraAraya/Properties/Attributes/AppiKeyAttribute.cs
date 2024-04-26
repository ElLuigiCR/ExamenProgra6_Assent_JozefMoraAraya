using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExamenProgra6_Assent_JozefMoraAraya.Properties.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public class AppiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _apiKey = "P6ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida))
            {

                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "No contiene informacion de seguridad"
                };

                return;

                var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

                var ApikeyValue = appSettings.GetValue<string>(_apiKey);

                if (!ApikeyValue.Equals(ApiSalida))
                {

                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = "Datos incorrectos de ApiKey"
                    };
                    return;

                }

            }
        }


    }
}
