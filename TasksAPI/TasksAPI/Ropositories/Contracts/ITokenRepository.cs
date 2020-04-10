using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAPI.Models;

namespace TasksAPI.Ropositories.Contracts
{
    public interface ITokenRepository : ICrudBaseRepository<TokenModel>
    {
        TokenModel GetEntity(string refreshToken);
    }
}
