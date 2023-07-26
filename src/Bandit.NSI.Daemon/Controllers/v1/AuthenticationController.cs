using Bandit.NSI.Daemon.Models.DTOs;
using Bandit.NSI.Daemon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bandit.NSI.Daemon.Controllers;

[ApiController]
[Route("auth")]
[Produces("application/json")]
public class AuthenticationController : ControllerBase
{
    private readonly IAccountsService _accountsService;

    public AuthenticationController(IAccountsService accountsService)
    {
        _accountsService = accountsService;
    }

    /// <summary>
    /// Logs in a user and returns an access token.
    /// </summary>
    /// <param name="loginDTO">The user's login credentials</param>
    /// <response code="200">Returns the access token if login was successful</response>
    /// <response code="401">If the login credentials are invalid. Documentation available at: https://github.com/TristesseLOL/bandit-nsi/blob/master/documentation/errors.md#sparkle</response>
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(SessionTokenDTO), 200)]
    [ProducesResponseType(typeof(ProblemDetailDTO), 401)]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        string ip = HttpContext.Connection.RemoteIpAddress.ToString();
        var token = await _accountsService.Login(loginDTO, ip);
        return Ok(token);
    }

    /// <summary>
    /// Registers a new user and returns an access token.
    /// </summary>
    /// <param name="registerDto">The user's registration details</param>
    /// <returns>An access token if registration was successful</returns>
    /// <response code="200">Returns the access token if registration was successful</response>
    /// <response code="409">If the email provided is already registered. Documentation available at: https://github.com/TristesseLOL/bandit-nsi/blob/master/documentation/errors.md#glowfish</response>
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(SessionTokenDTO), 200)]
    [ProducesResponseType(typeof(ProblemDetailDTO), 409)]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        string ip = HttpContext.Connection.RemoteIpAddress.ToString();
        var token = await _accountsService.Register(registerDto, ip);
        return Ok(token);
    }
}
