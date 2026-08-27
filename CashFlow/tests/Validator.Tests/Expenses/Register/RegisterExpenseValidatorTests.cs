using CashFlow.Application.UseCases.Expenses;
using CashFlow.Application.UseCases.Expenses.Register;
using CommonTestUtilities.Requests;

namespace Validator.Tests.Expenses.Register
{
    public class RegisterExpenseValidatorTests
    {
        [Fact]
        public void Sucess()
        {
            var validator = new ExpenseValidator();
            var request = RequestRegisterExpensesJsonBuilder.Build();
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }
        [Fact]
        public void Error_Title()
        {
            var validator = new ExpenseValidator();
            var request = RequestRegisterExpensesJsonBuilder.Build();
            request.Title = "";
            var result = validator.Validate(request);
            Assert.False(result.IsValid);
        }
    }
}
