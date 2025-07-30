using Identity.Dal.Entities;

namespace Identity.Repository.Repositories
{
    public interface IUserRepository
    {
        public Task<long> InsertAsync(User user);
        Task<User?> SelectUserByUserNameAsync(string userName);

        Task<User?> SelectUserByUserEmailAsync(string email);
    }
}