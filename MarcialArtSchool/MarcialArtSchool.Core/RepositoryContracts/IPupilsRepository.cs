using MarcialArtSchool.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcialArtSchool.Core.RepositoryContracts;

interface IPupilsRepository
{
    public Task<Pupils> GetPupilByIdAsync(int id);
    public Task<List<Pupils>> GetAllPupilsAsync();
    public Task AddPupilAsync(Pupils pupil);
}

