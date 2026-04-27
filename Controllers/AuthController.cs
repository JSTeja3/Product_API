using Microsoft.AspNetCore.Mvc;
using Product_API.Services.Interface;

namespace Product_API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string userId, string password)
        {
            if (userId != "admin" || password != "123")
                return Unauthorized();

            var accessToken = _tokenService.GenerateAccessToken(userId);
            var refreshToken = await _tokenService.GenerateRefreshTokenandSaveAsync(userId);



            return Ok(new
            {
                accessToken,
                refreshToken

            });


        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(string userId, string refreshToken)
        {
            bool isValid = await _tokenService.ValidateRefreshToken(userId, refreshToken);
            if (!isValid)
            {
                return Unauthorized();
            }
            var newAccessToken = _tokenService.GenerateAccessToken(userId);

            return Ok(new
            {
                accessToken = newAccessToken
            });


        }
        
    }
}