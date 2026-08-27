using AutoMapper;
using CashFlow.Application.AutoMapper;
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
        private readonly IExpensesWriteOnlyRepository _repository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _autoMapper;
        public RegisterExpenseUseCase(IExpensesWriteOnlyRepository repository, IUnityOfWork unityOfWork, IMapper autoMapper)
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
            _autoMapper = autoMapper;
        }
        public async Task<ResponseRegisteredExpenseJson> Execute(RequestExpensesJson request)
        {
            Validate(request);

            var entity = _autoMapper.Map<Expense>(request);
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
