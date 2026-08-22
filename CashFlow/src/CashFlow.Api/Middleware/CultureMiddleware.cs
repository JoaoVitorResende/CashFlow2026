using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace CashFlow.Application.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var acceptLanguage = context.Request.Headers["Accept-Language"].FirstOrDefault();

            var culture = "pt-BR"; // fallback padrão

            if (!string.IsNullOrWhiteSpace(acceptLanguage))
            {
                // pega só o primeiro idioma da lista, antes da vírgula, e remove o ";q=0.9" se tiver
                culture = acceptLanguage.Split(',')[0].Split(';')[0].Trim();
            }

            CultureInfo cultureInfo;
            try
            {
                cultureInfo = new CultureInfo(culture);
            }
            catch (CultureNotFoundException)
            {
                cultureInfo = new CultureInfo("pt-BR"); // fallback se vier algo inválido mesmo assim
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
