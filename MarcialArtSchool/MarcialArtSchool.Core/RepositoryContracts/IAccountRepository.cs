using Account.DTO.LoginDTO;
using MarcialArtSchool.DTO.Pupils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcialArtSchool.Core.RepositoryContracts;
public interface IAccountRepository
{
    public Task<bool> GetPupilByUserPass(LoginDTO loginDTO);
    public Task<bool> AddPupilCredentials(LoginDTO loginDTO);


}

