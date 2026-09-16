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
        async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(User user, long id)
        {
            return await _dbcontext.Expenses.FirstOrDefaultAsync(e => e.Id == id && e.UserId == user.Id);
        }
        public void Update(Expense expense)
        {
            _dbcontext.Expenses.Update(expense);
        }
        public async Task<List<Expense>> FilterByMonth(DateOnly date)
        {
            var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
            var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
            var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

            return await _dbcontext.Expenses.AsNoTracking()
                .Where(expnese => expnese.Date >= startDate && expnese.Date <= endDate)
                .OrderBy(expense => expense.Date)
                .ToListAsync();
        }
    }
}
