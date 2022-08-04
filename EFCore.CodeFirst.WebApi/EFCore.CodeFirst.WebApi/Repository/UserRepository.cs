using EFCore.CodeFirst.WebApi.Contexts;
using EFCore.CodeFirst.WebApi.DTO;
using EFCore.CodeFirst.WebApi.Models;
using EFCore.CodeFirst.WebApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.CodeFirst.WebApi.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public User AuthenticateUser(AuthenticateRequest user)
        {
            string hassedPassword = MD5Hash(user.Password);
            return this.applicationDbContext.Set<User>().Where( x => x.Username == user.Username && x.Password == hassedPassword).FirstOrDefault();
         }
        public User GetUserById(int id)
        {
            return this.applicationDbContext.Set<User>().Where(x => x.Id == id).FirstOrDefault();
        }

        public int NumberOfUsers()
        {
            return FindAll().Count();
        }
    }
}
