using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories
{
    internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpensesUpdateOnlyRepository
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

        public async Task<bool> Delete(long id)
        {
            var result = await _dbcontext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
            if (result is null)
                return false;
            _dbcontext.Expenses.Remove(result);
            return true;
        }

        public async Task<List<Expense>> GetAll()
        {
            // if the item going to change the data use asnotracking he dosen't use cache and boost performace
            return await _dbcontext.Expenses.AsNoTracking().ToListAsync();
        }

        async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
        {
            return await _dbcontext.Expenses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }
        async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
        {
            return await _dbcontext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(Expense expense)
        {
            _dbcontext.Expenses.Update(expense);
        }
    }
}
