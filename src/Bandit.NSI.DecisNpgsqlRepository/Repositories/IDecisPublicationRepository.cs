using Bandit.NSI.DecisNpgsqlRepository.Models;

namespace Bandit.NSI.DecisNpgsqlRepository.Repositories
{
    public interface IDecisPublicationRepository
    {
        Task AddAsync(Publication publication);
        Task<Publication> GetByIdAsync(Guid id);
        Task<List<Publication>> GetAllAsync();
    }
}
