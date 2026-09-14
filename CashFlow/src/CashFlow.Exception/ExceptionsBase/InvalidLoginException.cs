namespace CashFlow.Exception.ExceptionsBase
{
    public class InvalidLoginException : CashFlowException
    {
        public InvalidLoginException() : base(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
        {
        }
        public override int StatusCode => throw new NotImplementedException();
        public override List<string> GetErros()
        {
            return [Message];
        }
    }
}
