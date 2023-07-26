using Bandit.NSI.TempNpgsqlRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace Bandit.NSI.TempNpgsqlRepository.Repositories
{
    public class TempStudyRepository : ITempStudyRepository
    {
        private readonly TempNpgsqlDbContext _context;
        private readonly DbSet<Study> _studies;

        public TempStudyRepository(TempNpgsqlDbContext context)
        {
            _context = context;
            _studies = context.Studies;
        }

        public async Task AddAsync(Study study)
        {
            await _studies.AddAsync(study);
            await _context.SaveChangesAsync();
        }

        public Task<List<Study>> GetAllAsync() => _studies.Where(s => s.Status != Status.Published).ToListAsync();

        public async Task<Study?> GetByIdAsync(Guid studyId) => await _studies.Where(s => s.Id == studyId).Include(s => s.Comments).FirstOrDefaultAsync();

        public async Task UpdateAsync(Study study)
        {
            _studies.Update(study);
            await _context.SaveChangesAsync();
        }
    }
}
