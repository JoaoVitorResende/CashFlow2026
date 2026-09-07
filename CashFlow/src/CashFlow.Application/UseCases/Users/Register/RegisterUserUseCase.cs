using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Users.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IMapper _autoMapper;

        public RegisterUserUseCase(IUserWriteOnlyRepository repository, IUnityOfWork unityOfWork, IMapper autoMapper)
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
            _autoMapper = autoMapper;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUsersJson request)
        {
            Validate(request);
            var user = _autoMapper.Map<User>(request);
            await _repository.Add(user);
            await _unityOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
            };
        }

        private void Validate(RequestRegisterUsersJson request)
        {
            var validator = new UserValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
