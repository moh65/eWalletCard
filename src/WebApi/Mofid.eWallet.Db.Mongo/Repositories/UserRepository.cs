using Mofid.eWallet.Entities.BusinessObjects;
using Mofid.eWallet.Infra.Contracts.RepositoriesContracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mofid.eWallet.Db.Mongo
{
	public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;
        public UserRepository(IDatabaseContext dbContext)
        {
            _userCollection = dbContext.GetCollection<User>(nameof(User));
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await _userCollection.Find(a => a.Username == username).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            var user = await _userCollection.Find(a => a.Id == userId).FirstOrDefaultAsync();
            return user;
        }
    }
}
