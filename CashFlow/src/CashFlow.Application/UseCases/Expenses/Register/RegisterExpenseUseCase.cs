using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {
        public ResponseRegisteredExpenseJson Execute(RequestRegisterExpensesJson request)
        {
            Validate(request);
            var entity = new Expense {
                Title = request.Title,
                Amount = request.Amount,
                Date = request.Date,
                Description = request.Description,
                PaymentType = (Domain.Enuns.PaymentType)request.PaymentType
            };
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
