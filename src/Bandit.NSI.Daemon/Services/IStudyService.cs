using Bandit.NSI.Daemon.Models;
using Bandit.NSI.Daemon.Models.DTOs;

namespace Bandit.NSI.Daemon.Services
{
    public interface IStudyService
    {
        Task<Guid> CreateAsync(StudyCreationDTO studyCreationDTO, Account account);
        Task UpdateAsync(StudyCreationDTO studyCreationDTO, Account account);
        Task CommentAsync(CommentDTO commentDTO, Account account);
        Task PublishAsync(Guid studyId);
        Task<CompleteStudyDTO> GetPendingByIdAsync(Guid id);
        Task<CompleteStudyDTO> GetPublishedByIdAsync(Guid id);
        Task<List<StudyResumeDTO>> GetAllPendingAsync();
        Task<List<StudyResumeDTO>> GetAllPublishedAsync();
    }
}
