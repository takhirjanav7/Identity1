using Identity.Bll.DTOs.UserDTOs;

namespace Identity.Bll.Services
{
    public interface IUserService
    {
        Task<long> PostAsync(UserCreateDTO userCreateDto);
    }
}