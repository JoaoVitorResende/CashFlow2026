using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class ExpensesRepository : IExpensesRepository
    {
        private readonly CashFlowDbContext _dbcontext;
        public ExpensesRepository(CashFlowDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task Add(Expense expense)
        {
            await _dbcontext.Expenses.AddAsync(expense);
        }
    }
}
