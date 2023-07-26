using Bandit.NSI.TempNpgsqlRepository.Models;

namespace Bandit.NSI.TempNpgsqlRepository.Repositories
{
    public interface ITempStudyRepository
    {
        Task AddAsync(Study study);
        Task<List<Study>> GetAllAsync();
        Task<Study?> GetByIdAsync(Guid studyId);
        Task UpdateAsync(Study study);
    }
}
