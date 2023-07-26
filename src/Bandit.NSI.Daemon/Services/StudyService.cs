using Bandit.NSI.Daemon.Exceptions;
using Bandit.NSI.Daemon.Models;
using Bandit.NSI.Daemon.Models.DTOs;
using Bandit.NSI.DecisNpgsqlRepository.Models;
using Bandit.NSI.DecisNpgsqlRepository.Repositories;
using Bandit.NSI.TempNpgsqlRepository.Models;
using Bandit.NSI.TempNpgsqlRepository.Repositories;

namespace Bandit.NSI.Daemon.Services
{
    public class StudyService : IStudyService
    {
        private readonly ITempStudyRepository _tempStudyRepository;
        private readonly IDecisPublicationRepository _decisPublicationRepository;

        public StudyService(ITempStudyRepository studyRepository, IDecisPublicationRepository publicationRepository)
        {
            _tempStudyRepository = studyRepository;
            _decisPublicationRepository = publicationRepository;
        }

        public async Task<Guid> CreateAsync(StudyCreationDTO studyCreationDTO, Account account)
        {
            var id = Guid.NewGuid();
            var study = new Study
            {
                Id = id,
                Title = studyCreationDTO.Title,
                Description = studyCreationDTO.Description,
                Content = studyCreationDTO.Content,
                Tags = studyCreationDTO.Tags,
                AuthorName = $"{account.FirstName} {account.LastName}",
                CreationDate = DateTime.UtcNow,
                Comments = new List<Comment>()
            };
            await _tempStudyRepository.AddAsync(study);
            return id;
        }

        public async Task UpdateAsync(StudyCreationDTO studyCreationDTO, Account account)
        {
            var study = await _tempStudyRepository.GetByIdAsync(studyCreationDTO.StudyId);

            study.Title = studyCreationDTO.Title;
            study.Description = studyCreationDTO.Description;
            study.Content = studyCreationDTO.Content;
            study.Tags = studyCreationDTO.Tags;

            await _tempStudyRepository.UpdateAsync(study);
        }

        public async Task CommentAsync(CommentDTO commentDTO, Account account)
        {
            var study = await _tempStudyRepository.GetByIdAsync(commentDTO.StudyId);
            if (study == null) throw new StudyNotFoundException(commentDTO.StudyId);
            study.Comments.Add(new()
            {
                Content = commentDTO.Content,
                AuthorName = $"{account.FirstName} {account.LastName}",
                Date = DateTime.UtcNow
            });
            await _tempStudyRepository.UpdateAsync(study);
        }

        public async Task PublishAsync(Guid studyId)
        {
            var study = await _tempStudyRepository.GetByIdAsync(studyId);
            var publication = new Publication
            {
                Title = study.Title,
                Description = study.Description,
                Content = study.Content,
                Tags = study.Tags,
                AuthorName = study.AuthorName,
                CreationDate = study.CreationDate
            };
            await _decisPublicationRepository.AddAsync(publication);

            study.Status = Status.Published;
            await _tempStudyRepository.UpdateAsync(study);
        }

        public async Task<CompleteStudyDTO> GetPendingByIdAsync(Guid id)
        {
            var study = await _tempStudyRepository.GetByIdAsync(id);
            if (study == null) throw new StudyNotFoundException(id);
            return new CompleteStudyDTO
            {
                Id = study.Id,
                Title = study.Title,
                Description = study.Description,
                Content = study.Content,
                Tags = study.Tags,
                AuthorName = study.AuthorName,
                CreationDate = study.CreationDate,
                Status = study.Status,
                Comments = study.Comments,
            };
        }

        public async Task<CompleteStudyDTO> GetPublishedByIdAsync(Guid id)
        {
            var publication = await _decisPublicationRepository.GetByIdAsync(id);
            if (publication == null) throw new StudyNotFoundException(id);
            return new CompleteStudyDTO
            {
                Id = publication.Id,
                Title = publication.Title,
                Description = publication.Description,
                Content = publication.Content,
                Tags = publication.Tags,
                AuthorName = publication.AuthorName,
                CreationDate = publication.CreationDate
            };
        }

        public async Task<List<StudyResumeDTO>> GetAllPendingAsync()
        {
            var studies = await _tempStudyRepository.GetAllAsync();
            var resume = studies.Select(s => new StudyResumeDTO
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Tags = s.Tags,
                Date = s.CreationDate,
                AuthorName = s.AuthorName,
            }).ToList();
            return resume;
        }

        public async Task<List<StudyResumeDTO>> GetAllPublishedAsync()
        {
            var studies = await _decisPublicationRepository.GetAllAsync();
            var resume = studies.Select(s => new StudyResumeDTO
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Tags = s.Tags,
                Date = s.CreationDate,
                AuthorName = s.AuthorName,
            }).ToList();
            return resume;
        }
    }
}
