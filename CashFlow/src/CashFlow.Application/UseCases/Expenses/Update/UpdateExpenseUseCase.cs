using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update
{
    public class UpdateExpenseUseCase : IUpdateExpenseUseCase
    {
        private readonly IExpensesUpdateOnlyRepository _repository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        public UpdateExpenseUseCase(IExpensesUpdateOnlyRepository repository, IUnityOfWork unityOfWork, IMapper mapper, ILoggedUser loggedUser)
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }
        public async Task Execute(long id, RequestExpensesJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.Get();

            var expense = await _repository.GetById(loggedUser, id);

            if (expense is null || loggedUser.Id != expense.UserId)
            {
                throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }
            // in this form of map it keeps the expense found
            _mapper.Map(request, expense);
            _repository.Update(expense);

            await _unityOfWork.Commit();
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
