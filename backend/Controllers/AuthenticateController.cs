using Mappa.Dtos;
using Mappa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mappa.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public UserController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _authenticationService.Login(request);
            return Ok(response);
        }
        catch (ArgumentException ex) when (ex.Message.Contains($"Unable to authenticate user {request.UserName}"))
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }

    }

    // [HttpPost("register")]
    // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    // {
    //     try
    //     {
    //         var response = await _authenticationService.Register(request);
    //         return Ok(response);
    //     }
    //     catch (ArgumentException ex) when (ex.Message.Contains($"User with email {request.Email} or username {request.UserName} already exists."))
    //     {
    //         return Conflict(new { Message = ex.Message });
    //     }
    //     catch (Exception ex)
    //     {
    //         // Log the exception (optional)
    //         return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
    //     }
    // }
}