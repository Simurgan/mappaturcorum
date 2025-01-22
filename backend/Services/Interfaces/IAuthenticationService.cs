using System.IdentityModel.Tokens.Jwt;
using Mappa.Dtos;

namespace Mappa.Services;

public interface IAuthenticationService
{
    Task<string> Register(RegisterRequest request);
    Task<string> Login(LoginRequest request);
}