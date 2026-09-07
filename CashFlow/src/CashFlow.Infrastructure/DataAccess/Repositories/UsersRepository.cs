using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Users;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    public class UsersRepository: IUserWriteOnlyRepository
    {
        private readonly CashFlowDbContext _dbcontext;

        public async Task Add(User user)
        {
            await _dbcontext.Users.AddAsync(user);
        }
    }
}
