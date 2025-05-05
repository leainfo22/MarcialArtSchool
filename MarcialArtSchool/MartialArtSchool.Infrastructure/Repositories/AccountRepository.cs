using Dapper;
using MarcialArtSchool.Core.RepositoryContracts;
using MarcialArtSchool.Core.Utils;
using MarcialArtSchool.DTO.Pupils;
using MartialArtSchool.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartialArtSchool.Infrastructure.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly DapperDbContext _dbContext;
        public AccountRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> GetPupilByUserPass(string user, string password) 
        {
            var hash = Hash.HashPasswordWithSalt(password);
            string query = $"EXEC dbo.CheckUserExistsByUsernameAndPassword @Username = '{user}', @UserPassword = '{hash}'";
            int rowCountAffected = await _dbContext.DbConnection.ExecuteAsync(query);

            if (rowCountAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> AddPupilCredentials(string user, string password)
        {
            var hash = Hash.HashPasswordWithSalt(password);
            Guid pupilId = Guid.NewGuid();
            string query = $"EXEC @UserExistsResult = dbo.CheckUserExistsByUsernameAndPassword @UserId = '{pupilId}', @UserUser = '{user}', @UserPassword = '{hash}', @PupilId ='{pupilId}'";
            int rowCountAffected = await _dbContext.DbConnection.ExecuteAsync(query);

            if (rowCountAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
