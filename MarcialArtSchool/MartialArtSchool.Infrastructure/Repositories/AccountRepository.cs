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
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection;
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
        public async Task<bool> CheckPupilByUserPass(LoginDTO loginDTO) 
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
        public async Task<Pupils> GetPupilByUserPass(LoginDTO loginDTO)
        {
            Pupils pupils = new Pupils();
            try
            {
                var hash = Hash.HashPasswordWithSalt(loginDTO.UserPassword);
                loginDTO.UserPassword = hash;

                var parameters = new DynamicParameters();
                parameters.Add("@Username", loginDTO.Username);
                parameters.Add("@UserPassword", loginDTO.UserPassword);
                parameters.Add("@FirstName", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                parameters.Add("@LastName", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                parameters.Add("@UserType", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                parameters.Add("@Grade", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                parameters.Add("@Gender", dbType: DbType.String, direction: ParameterDirection.Output, size: 10);
                parameters.Add("@BirthDate", dbType: DbType.Date, direction: ParameterDirection.Output);
                parameters.Add("@Phone", dbType: DbType.String, direction: ParameterDirection.Output, size: 20);
                parameters.Add("@DNI", dbType: DbType.String, direction: ParameterDirection.Output, size: 20);
                parameters.Add("@Adress", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("@ProvinceState", dbType: DbType.String, direction: ParameterDirection.Output, size: 100);
                parameters.Add("@Country", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                parameters.Add("@BodyWeight", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                parameters.Add("@Height", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);
                parameters.Add("@Pathology", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                parameters.Add("@Judge", dbType: DbType.String, direction: ParameterDirection.Output, size: 50);

                int? rowCountAffected = await _dbContext.DbConnection.ExecuteScalarAsync<int?>(
                    "dbo.CheckUserExistsByUsernameAndPassword",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

                pupils.FirstName    = parameters.Get<string>("@FirstName");
                pupils.FirstName    =   parameters.Get<string>("@FirstName");
                pupils.LastName		=   parameters.Get<string>("@LastName");
                pupils.UserType		=   parameters.Get<string>("@UserType");
                pupils.Grade		=	parameters.Get<string>("@Grade");
                pupils.Gender		=	parameters.Get<string>("@Gender");
                pupils.BirthDate	=	parameters.Get<DateTime>("@BirthDate");
                pupils.Phone		=	parameters.Get<string>("@Phone");
                pupils.DNI			=   parameters.Get<string>("@DNI");
                pupils.Adress		=	parameters.Get<string>("@Adress");
                pupils.ProvinceState=	parameters.Get<string>("@ProvinceState ");
                pupils.Country		=   parameters.Get<string>("@Country");
                pupils.BodyWeight	=	parameters.Get<string>("@BodyWeight");
                pupils.Height		=	parameters.Get<string>("@Height");
                pupils.Pathology	=	parameters.Get<string>("@Pathology");
                pupils.Judge        = parameters.Get<string>("@Judge");
                                              
                return pupils;
                
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
        public async Task<Pupils?> Login(LoginDTO loginRequest)
        {
            Pupils pupil = await GetPupilByUserPass(loginRequest);
            if (pupil == null)
            {
                return new Pupils { };
            }
            else
                return pupil;
        }
    }
}
