using Bandit.NSI.Daemon.Models.DTOs;
using Bandit.NSI.Daemon.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bandit.NSI.Daemon.Controllers.v1
{
    [ApiController]
    [Route("study")]
    [Produces("application/json")]
    public class StudyController : ControllerBase
    {
        private readonly IStudyService _studyService;
        private readonly ITokenService _tokenService;

        public StudyController(IStudyService studyService, ITokenService tokenService)
        {
            _studyService = studyService;
            _tokenService = tokenService;
        }


        /// <summary>
        /// Creates a new study
        /// </summary>
        /// <param name="studyCreationDTO">The values of the study to be created</param>
        /// <response code="200">Return a 200 HTTP status code if the study was successfully created</response>
        /// <response code="401">If the provided token is not valid. Documentation available at: https://github.com/TristesseLOL/bandit-nsi/blob/master/documentation/errors.md#sparkle</response>
        [Authorize(Roles = "DataScientist,ChiefDataScientist,Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(typeof(ProblemDetailDTO), 401)]
        public async Task<IActionResult> Create([FromBody] StudyCreationDTO studyCreationDTO)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var account = await _tokenService.GetAccountAsync(token);
            var studyId = await _studyService.CreateAsync(studyCreationDTO, account);
            return Ok(studyId);
        }

        /// <summary>
        /// Updates a study
        /// </summary>
        /// <param name="studyCreationDTO">The values of the study to be updated</param>
        /// <response code="200">Return a 200 HTTP status code if the study was successfully updated</response>
        /// <response code="401">If the provided token is not valid. Documentation available at: https://github.com/TristesseLOL/bandit-nsi/blob/master/documentation/errors.md#sparkle</response>
        [Authorize(Roles = "DataScientist,ChiefDataScientist,Admin")]
        [HttpPut]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(typeof(ProblemDetailDTO), 401)]
        public async Task<IActionResult> Update([FromBody] StudyCreationDTO studyCreationDTO)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var account = await _tokenService.GetAccountAsync(token);
            await _studyService.UpdateAsync(studyCreationDTO, account);
            return Ok();
        }

        /// <summary>
        /// Publish a study
        /// </summary>
        /// <param name="studyCreationDTO">The values of the study to be created</param>
        /// <response code="200">Return a 200 HTTP status code if the study was successfully published</response>
        /// <response code="401">If the provided token is not valid. Documentation available at: https://github.com/TristesseLOL/bandit-nsi/blob/master/documentation/errors.md#sparkle</response>
        [Authorize(Roles = "ChiefDataScientist,Admin")]
        [HttpPost("publish")]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(typeof(ProblemDetailDTO), 401)]
        public async Task<IActionResult> Publish([FromBody] Guid studyId)
        {
            await _studyService.PublishAsync(studyId);
            return Ok();
        }

        /// <summary>
        /// Adds a new comment to a study
        /// </summary>
        /// <param name="commentDTO">The comment</param>
        /// <response code="200">Return a 200 HTTP status code if the comment was successfully added</response>
        /// <response code="401">If the provided token is not valid. Documentation available at: https://github.com/TristesseLOL/bandit-nsi/blob/master/documentation/errors.md#sparkle</response>
        /// <response code="404">If the related study could not be found.</response>
        [Authorize(Roles = "DataScientist,ChiefDataScientist,Admin")]
        [HttpPatch("comment")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetailDTO), 401)]
        [ProducesResponseType(typeof(ProblemDetailDTO), 404)]
        public async Task<IActionResult> Comment([FromBody] CommentDTO commentDTO)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var account = await _tokenService.GetAccountAsync(token);
            await _studyService.CommentAsync(commentDTO, account);
            return Ok();
        }

        /// <summary>
        /// Gets a specific study
        /// </summary>
        /// <response code="200">Returns the requested study details/response>
        /// <response code="401">If the provided token is not valid. Documentation available at: https://github.com/TristesseLOL/bandit-nsi/blob/master/documentation/errors.md#sparkle</response>
        /// <response code="404">If the related study could not be found.</response>
        [Authorize(Roles = "DataScientist,ChiefDataScientist,Admin")]
        [HttpGet("pending/details/{id}")]
        [ProducesResponseType(typeof(List<StudyResumeDTO>), 200)]
        [ProducesResponseType(typeof(ProblemDetailDTO), 401)]
        public async Task<IActionResult> GetPendingPublication([FromRoute] Guid id) => Ok(await _studyService.GetPendingByIdAsync(id));

        /// <summary>
        /// Gets a specific study
        /// </summary>
        /// <response code="200">Returns the requested study details/response>
        /// <response code="401">If the provided token is not valid. Documentation available at: https://github.com/TristesseLOL/bandit-nsi/blob/master/documentation/errors.md#sparkle</response>
        /// <response code="404">If the related study could not be found.</response>
        [AllowAnonymous]
        [HttpGet("published/details/{id}")]
        [ProducesResponseType(typeof(List<StudyResumeDTO>), 200)]
        [ProducesResponseType(typeof(ProblemDetailDTO), 401)]
        public async Task<IActionResult> GetPublishedPublication([FromRoute] Guid id) => Ok(await _studyService.GetPublishedByIdAsync(id));

        /// <summary>
        /// Gets all the studies resume
        /// </summary>
        /// <response code="200">Returns all the studies resume</response>
        /// <response code="401">If the provided token is not valid. Documentation available at: https://github.com/TristesseLOL/bandit-nsi/blob/master/documentation/errors.md#sparkle</response>
        [AllowAnonymous]
        [HttpGet("published")]
        [ProducesResponseType(typeof(List<StudyResumeDTO>), 200)]
        [ProducesResponseType(typeof(ProblemDetailDTO), 401)]
        public async Task<IActionResult> GetAllPublished()
        {
            var studies = await _studyService.GetAllPublishedAsync();
            return Ok(studies.OrderByDescending(s => s.Date));
        }

        /// <summary>
        /// Gets all the studies resume
        /// </summary>
        /// <response code="200">Returns all the studies resume</response>
        /// <response code="401">If the provided token is not valid. Documentation available at: https://github.com/TristesseLOL/bandit-nsi/blob/master/documentation/errors.md#sparkle</response>
        [AllowAnonymous]
        [HttpGet("pending")]
        [ProducesResponseType(typeof(List<StudyResumeDTO>), 200)]
        [ProducesResponseType(typeof(ProblemDetailDTO), 401)]
        public async Task<IActionResult> GetPending()
        {
            var studies = await _studyService.GetAllPendingAsync();
            return Ok(studies.OrderByDescending(s => s.Date));
        }

        /// <summary>
        /// Gets the public latest resumes
        /// </summary>
        /// <response code="200">Returns the latest public resumes/response>
        /// <response code="401">If the provided token is not valid. Documentation available at: https://github.com/TristesseLOL/bandit-nsi/blob/master/documentation/errors.md#sparkle</response>
        [AllowAnonymous]
        [HttpGet("latest")]
        [ProducesResponseType(typeof(List<StudyResumeDTO>), 200)]
        [ProducesResponseType(typeof(ProblemDetailDTO), 401)]
        public async Task<IActionResult> GetLatest()
        {
            var studies = await _studyService.GetAllPublishedAsync();
            return Ok(studies.OrderByDescending(s => s.Date).Take(3));
        }
    }
}
