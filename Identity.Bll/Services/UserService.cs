using FluentValidation;
using Identity.Bll.Converter;
using Identity.Bll.DTOs.UserDTOs;
using Identity.Repository.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserRepository;
        private readonly IValidator<UserCreateDTO> Validator;
        private readonly ILogger<UserService> Logger;
        public UserService(IUserRepository userRepository, IValidator<UserCreateDTO> validator, ILogger<UserService> logger)
        {
            UserRepository = userRepository;
            Validator = validator;
            Logger = logger;
        }
        public async Task<long> PostAsync(UserCreateDTO userCreateDto)
        {
            var result = Validator.Validate(userCreateDto);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var user = Mappings.ConvertToUser(userCreateDto);
            var userId = await UserRepository.InsertAsync(user);

            return userId;
        }
    }
}
