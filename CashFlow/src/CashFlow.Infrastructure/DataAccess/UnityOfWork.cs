using CashFlow.Domain.Repositories;

namespace CashFlow.Infrastructure.DataAccess
{
    internal class UnityOfWork : IUnityOfWork
    {
        private readonly CashFlowDbContext _dbcontext;
        public UnityOfWork(CashFlowDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public void Commit()
        {
            _dbcontext.SaveChanges();
        }
    }
}
