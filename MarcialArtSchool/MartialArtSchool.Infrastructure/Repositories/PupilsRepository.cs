using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartialArtSchool.Infrastructure.Repositories;
internal class PupilsRepository : IPupilsRepository
{
    private readonly List<Pupils> _pupils;
    public PupilsRepository()
    {
        _pupils = new List<Pupils>();
    }
    public Task<Pupils> GetPupilByIdAsync(int id)
    {
        var pupil = _pupils.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(pupil);
    }
    public Task<List<Pupils>> GetAllPupilsAsync()
    {
        return Task.FromResult(_pupils);
    }
    public Task AddPupilAsync(Pupils pupil)
    {
        _pupils.Add(pupil);
        return Task.CompletedTask;
    }
}

