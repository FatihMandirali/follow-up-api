using Follow_Up.Application.Repositories;
using Follow_Up.Application.Services.Interfaces;
using Follow_Up.Domain;
using Follow_Up.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Infrastructure.Services
{
    public class UserService : Repository<User>, IUserService
    {
        public UserService(FollowUpDbContext context) : base(context)
        {
        }

        public async Task<User> FindUserByEmail(string email)
        {
            var user = await FindAsync(x => x.Email == email);
            return user;
        }

        public async Task<User> FindUserById(int id)
        {
            var user = await FindAsync(x => x.Id == id);
            return user;
        }
    }
}
