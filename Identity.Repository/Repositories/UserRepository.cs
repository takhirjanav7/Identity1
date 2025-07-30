using Identity.Dal;
using Identity.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MainContext MainContext;

        public UserRepository(MainContext mainContext)
        {
            MainContext = mainContext;
        }

        public async Task<long> InsertAsync(User user)
        {
            await MainContext.Users.AddAsync(user);
            await MainContext.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<User?> SelectUserByUserEmailAsync(string email)
        {
            var user = await MainContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<User?> SelectUserByUserNameAsync(string userName)
        {
            var user = await MainContext.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);

            return user;
        }
    }
}
