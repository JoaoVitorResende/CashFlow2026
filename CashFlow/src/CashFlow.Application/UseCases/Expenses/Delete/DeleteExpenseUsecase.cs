using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Delete
{
    public class DeleteExpenseUsecase : IDeleteExpenseUsecase
    {
        private readonly IExpensesWriteOnlyRepository _repository;
        private readonly IUnityOfWork _unitOfWork;

        public DeleteExpenseUsecase(IExpensesWriteOnlyRepository repository, IUnityOfWork unityOfWork)
        {
            _repository = repository;
            _unitOfWork = unityOfWork;
        }
        public async Task Execute(long id)
        {
            var result = await _repository.Delete(id);

            if (!result)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }
            await _unitOfWork.Commit();
        }
    }
}
