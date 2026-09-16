using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase: IRegisterExpenseUseCase
    {
        private readonly IExpensesWriteOnlyRepository _repository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _autoMapper;
        private readonly ILoggedUser _loggedUser;

        public RegisterExpenseUseCase(IExpensesWriteOnlyRepository repository, IUnityOfWork unityOfWork, IMapper autoMapper, ILoggedUser loggedUser)
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
            _autoMapper = autoMapper;
            _loggedUser = loggedUser;
        }
        public async Task<ResponseRegisteredExpenseJson> Execute(RequestExpensesJson request)
        {
            Validate(request);
            var loggedUser = await _loggedUser.Get();
            var entity = _autoMapper.Map<Expense>(request);
            entity.UserId = loggedUser.Id;
            await _repository.Add(entity);
            await _unityOfWork.Commit();
            return _autoMapper.Map<ResponseRegisteredExpenseJson>(entity);
        }

        private void Validate(RequestExpensesJson request)
        {
            var validator = new ExpenseValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
