using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<Expense>> GetAll()
        {
            return await _dbcontext.Expenses.ToListAsync();
        }
    }
}
