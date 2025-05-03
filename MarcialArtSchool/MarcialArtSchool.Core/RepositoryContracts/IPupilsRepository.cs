using MarcialArtSchool.DTO.Pupils;
namespace MarcialArtSchool.Core.RepositoryContracts;

public interface IPupilsRepository
{
    public Task<Pupils> GetPupilByIdAsync(Guid id);
    public Task<List<Pupils>> GetAllPupilsAsync();
    public Task AddPupilAsync(Pupils pupil);
}

