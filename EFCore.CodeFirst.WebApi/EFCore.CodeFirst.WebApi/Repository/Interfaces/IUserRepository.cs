using EFCore.CodeFirst.WebApi.DTO;
using EFCore.CodeFirst.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User AuthenticateUser(AuthenticateRequest user);
        User GetUserById(int id);
        int NumberOfUsers();
    }
}
