using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksAPI.Models;
using TasksAPI.Ropositories.Contracts;

namespace TasksAPI.Ropositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void Create(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Create(ApplicationUser user, string password)
        {
            var result = _userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
                return;

            var stringBuilder = new StringBuilder();

            foreach (var erro in result.Errors)
            {
                stringBuilder.Append(erro.Description);
            }

            throw new Exception($"Revendedora(a) não cadastrado(a)! {stringBuilder.ToString()}");
        }

        public void Delete(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser GetEntity(string email, string password)
        {
            var user = _userManager.FindByEmailAsync(email).Result;

            if (_userManager.CheckPasswordAsync(user, password).Result)
                return user;

            throw new Exception("Usuário não localizado!");
        }

        public void Update(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
