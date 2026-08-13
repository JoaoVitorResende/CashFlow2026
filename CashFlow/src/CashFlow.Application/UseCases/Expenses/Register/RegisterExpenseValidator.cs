using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseValidator: AbstractValidator<RequestExpensesJson>
    {
        public RegisterExpenseValidator()
        {
            RuleFor(expense => expense.Title).NotEmpty().WithMessage("The title is required");
            RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("The amount must be greater then zero");
            RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The expense can't be from the future");
            RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("Payment type isn't valid");
        }
    }
}
