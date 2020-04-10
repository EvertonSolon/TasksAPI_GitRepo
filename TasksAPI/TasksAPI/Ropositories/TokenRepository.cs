using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksAPI.Database;
using TasksAPI.Models;
using TasksAPI.Ropositories.Contracts;

namespace TasksAPI.Ropositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly TasksContext _context;
        public TokenRepository(TasksContext context)
        {
            _context = context;        }

        public void Create(TokenModel token)
        {
            _context.Tokens.Add(token);
            _context.SaveChanges();
        }

        public void Delete(TokenModel token)
        {
            throw new NotImplementedException();
        }

        public ICollection<TokenModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public TokenModel GetEntity(string refreshToken)
        {
            var token = _context.Tokens.FirstOrDefault(x => x.RefreshToken == refreshToken);

            return token;
        }

        public TokenModel GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TokenModel token)
        {
            throw new NotImplementedException();
        }
    }
}
