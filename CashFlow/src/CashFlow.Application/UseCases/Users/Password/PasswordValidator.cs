using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;
using global::CashFlow.Exception;
using System.Text.RegularExpressions;

namespace CashFlow.Application.UseCases.Users.Password
{
    public class PassowordValidator<T> : PropertyValidator<T, string>
    {
        const string ERROR_MESSAGE = "ERRORMESSAGE";
        public override string Name => "PasswordValidator";
        protected override string GetDefaultMessageTemplate(string errorCode) => "{ERROR_MESSAGE}";

        public override bool IsValid(ValidationContext<T> context, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }
            if (password.Length < 8)
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }
            if (!Regex.IsMatch(password, @"[A-Z]+"))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }
            if (!Regex.IsMatch(password, @"[a-z]+"))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }
            if (!Regex.IsMatch(password, @"[0-9]+"))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }
            if (!Regex.IsMatch(password, @"[\!\?\*z\.]+"))
            {
                context.MessageFormatter.AppendArgument(ERROR_MESSAGE, ResourceErrorMessages.INVALID_PASSWORD);
                return false;
            }
            return true;
        }
    }
}
