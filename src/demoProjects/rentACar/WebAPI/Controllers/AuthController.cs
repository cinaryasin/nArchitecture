using Application.Features.Auths.Commends;
using Application.Features.Auths.Dtos;
using Application.Features.Brands.Commends.CreateBrand;
using Application.Features.Brands.Dtos;
using Core.Security.Dtos;
using Core.Security.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            RegisterCommand registerCommand = new RegisterCommand()
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            };

            RegisteredDto result = await Mediator.Send(registerCommand);
            SetRefreshTokenCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }

        private void SetRefreshTokenCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
            Response.Cookies.Append("refreshedToken", refreshToken.Token, cookieOptions);
        }
        

    }
}

