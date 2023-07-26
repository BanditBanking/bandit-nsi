using Bandit.NSI.DecisNpgsqlRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Bandit.NSI.DecisNpgsqlRepository.Repositories
{
    public class DecisPublicationRepository : IDecisPublicationRepository
    {
        private readonly DecisNpgsqlDbContext _context;
        private readonly DbSet<Publication> _publications;

        public DecisPublicationRepository(DecisNpgsqlDbContext context)
        {
            _context = context;
            _publications = context.Publications;
        }

        public async Task AddAsync(Publication publication)
        {
            await _publications.AddAsync(publication);
            await _context.SaveChangesAsync();
        }

        public async Task<Publication?> GetByIdAsync(Guid publicationId) => await _publications.Where(p => p.Id == publicationId).FirstOrDefaultAsync();

        public async Task<List<Publication>> GetAllAsync() => await _publications.ToListAsync();

    }
}
