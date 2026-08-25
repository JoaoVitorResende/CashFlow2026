using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll
{
    public class GetAllExpensesUseCase : IGetAllExpensesUseCase
    {
        private readonly IExpensesRepository _repository;
        private readonly IMapper _mapper;
        public GetAllExpensesUseCase(IExpensesRepository repository,IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ResponseExpensesJson> Execute()
        {
            var result = await _repository.GetAll();
            return new ResponseExpensesJson
            {
                Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
            };
        }
    }
}
