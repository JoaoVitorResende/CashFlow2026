namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public interface IDeleteExpenseUsecase
    {
        Task Execute(long id);
    }
}
