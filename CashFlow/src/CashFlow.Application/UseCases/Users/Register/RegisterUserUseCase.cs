using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserWriteOnlyRepository _repository;
        private readonly IUserReadOnlyRepository _repositoryRead;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IMapper _autoMapper;

        public RegisterUserUseCase(IUserWriteOnlyRepository repository, IUnityOfWork unityOfWork, IMapper autoMapper, IPasswordEncripter passwordEncripter, IUserReadOnlyRepository repositoryRead)
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
            _autoMapper = autoMapper;
            _passwordEncripter = passwordEncripter;
            _repositoryRead = repositoryRead;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUsersJson request)
        {
            await Validate(request);
            var user = _autoMapper.Map<User>(request);
            user.Password = _passwordEncripter.Encrypt(request.Password);
            await _repository.Add(user);
            await _unityOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
            };
        }

        private async Task Validate(RequestRegisterUsersJson request)
        {
            var validator = new UserValidator();
            var result = validator.Validate(request);

            var emailExists = await _repositoryRead.ExistActiveUserWithEmail(request.Email);

            if (emailExists)
            {
                result.Errors.Add(new ValidationFailure(string.Empty,ResourceErrorMessages.EMAILS_EXISTS));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
