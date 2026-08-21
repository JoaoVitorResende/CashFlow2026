using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase: IRegisterExpenseUseCase
    {
        private readonly IExpensesRepository _repository;
        private readonly IUnityOfWork _unityOfWork;
        public RegisterExpenseUseCase(IExpensesRepository repository, IUnityOfWork unityOfWork)
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
        }
        public async Task<ResponseRegisteredExpenseJson> Execute(RequestRegisterExpensesJson request)
        {
            Validate(request);

            var entity = new Expense {
                Title = request.Title,
                Amount = request.Amount,
                Date = request.Date,
                Description = request.Description,
                PaymentType = (Domain.Enuns.PaymentType)request.PaymentType
            };

            await _repository.Add(entity);
            await _unityOfWork.Commit();
            return new ResponseRegisteredExpenseJson();
        }

        private void Validate(RequestRegisterExpensesJson request)
        {
            var validator = new RegisterExpenseValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
