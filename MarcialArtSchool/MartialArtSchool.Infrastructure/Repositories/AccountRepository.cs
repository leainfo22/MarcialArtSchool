using Account.DTO.LoginDTO;
using Dapper;
using MarcialArtSchool.Core.RepositoryContracts;
using MarcialArtSchool.Core.Utils;
using MarcialArtSchool.DTO.Pupils;
using MartialArtSchool.Infrastructure.DbContext;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<bool> GetPupilByUserPass(LoginDTO loginDTO) 
        {
            try            
            {
                var hash = Hash.HashPasswordWithSalt(loginDTO.UserPassword);
                loginDTO.UserPassword = hash;

                var parameters = new DynamicParameters();
                parameters.Add("@Username", loginDTO.Username);
                parameters.Add("@UserPassword", loginDTO.UserPassword);
                parameters.Add("@ExistingUsername", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);

                int? rowCountAffected = await _dbContext.DbConnection.ExecuteScalarAsync<int?>(
                    "dbo.CheckUserExistsByUsernameAndPassword",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                string existingUsername = parameters.Get<string>("@ExistingUsername");

                if (!string.IsNullOrEmpty(existingUsername))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error in GetPupilByUserPass", ex);
            }
            
        }
        public async Task<bool> AddPupilCredentials(LoginDTO loginDTO)
        {
            var hash = Hash.HashPasswordWithSalt(loginDTO.UserPassword);
            Guid pupilId = Guid.NewGuid();
            string query = $"EXEC @UserExistsResult = dbo.CheckUserExistsByUsernameAndPassword @UserId = '{pupilId}', @UserUser = '{loginDTO.Username}', @UserPassword = '{hash}', @PupilId ='{pupilId}'";
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
