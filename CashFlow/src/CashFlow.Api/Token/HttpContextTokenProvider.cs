using CashFlow.Domain.Security.Tokens;

namespace CashFlow.Api.Token
{
    public class HttpContextTokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public HttpContextTokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string TokenOnRequest()
        {
            var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(authorization) || !authorization.StartsWith("Bearer "))
                return string.Empty;

            return authorization["Bearer ".Length..].Trim();
        }
    }
}
