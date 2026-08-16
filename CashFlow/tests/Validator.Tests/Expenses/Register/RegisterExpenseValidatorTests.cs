using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;

namespace Validator.Tests.Expenses.Register
{
    public class RegisterExpenseValidatorTests
    {
        [Fact]
        public void Sucess()
        {
            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpensesJson
            {
                Amount = 100,
                Date = DateTime.Now.AddDays(-1),
                Description = "blabla",
                Title = "Title",
                PaymentType = CashFlow.Communication.Enums.PaymentType.CreditCard
            };
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }
    }
}
