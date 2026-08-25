using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses.GetById
{
    public class GetExpenseById : IGetExpenseById
    {
        private readonly IExpensesRepository _repository;
        private readonly IMapper _mapper;
        public GetExpenseById(IExpensesRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ResponseExpenseJson> Execute(long id)
        {
            var result = await _repository.GetById(id);
            if (result is null)
            {
                //throw new NotFoundExeception(ResourceErrorMessages.EXPENSE_NOT_FOUND);
            }

            return _mapper.Map<ResponseExpenseJson>(result);
        }
    }
}
