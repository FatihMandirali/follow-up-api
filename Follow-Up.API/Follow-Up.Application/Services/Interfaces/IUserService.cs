using Follow_Up.Application.Repositories;
using Follow_Up.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Application.Services.Interfaces
{
    public interface IUserService : IRepository<User>
    {
        Task<User> FindUserById(int id);
        Task<User> FindUserByEmail(string email);
    }
}
