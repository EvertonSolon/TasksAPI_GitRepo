using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAPI.Models;

namespace TasksAPI.Ropositories.Contracts
{
    public interface IUserRepository : ICrudBaseRepository<ApplicationUser>
    {
        void Create(ApplicationUser user, string password);
        ApplicationUser GetEntity(string email, string password);
    }
}
