using MarcialArtSchool.DTO.Pupils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcialArtSchool.Core.RepositoryContracts;
public interface IAccountRepository
{
    public Task<bool> GetPupilByUserPass(string user, string password);
    public Task<bool> AddPupilCredentials(string user, string password);


}

