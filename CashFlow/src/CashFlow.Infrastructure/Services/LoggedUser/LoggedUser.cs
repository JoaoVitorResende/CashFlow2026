using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace CashFlow.Infrastructure.Services.LoggedUser
{
    internal class LoggedUser : ILoggedUser
    {
        private readonly CashFlowDbContext _dbcontext;
        private readonly ITokenProvider _tokenProvider;
        public LoggedUser(CashFlowDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbcontext = dbContext;
            _tokenProvider = tokenProvider;
        }

        public async Task<User> Get()
        {
            string token = _tokenProvider.TokenOnRequest();
            var tokenHandler = new JsonWebTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJsonWebToken(token);
            var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
            return await _dbcontext.Users.AsNoTracking()
                .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
        }
    }
}
