using AppCleanArchitecture.API.Models;
using AppCleanArchitecture.Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppCleanArchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        public TokenController(IAuthenticate authenticate,
                               IConfiguration configuration)
        {
            _authenticate = authenticate ??
                throw new ArgumentNullException(nameof(authenticate));
            _configuration = configuration;
        }

        [HttpPost("CreateUser")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        public async Task<ActionResult> CreateUser([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.RegisterUser(userInfo.Email, userInfo.Password);

            if (result)
            {
               return Ok($" User {userInfo.Email} was create succesfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);

            if (result)
            {
                return GenerateToken(userInfo);
                //return Ok($" User {userInfo.Email} login succesfully");
            }
            else 
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserToken GenerateToken(LoginModel userInfo)
        {
            //declaracao de usuario
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuvalor", "Irineu"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gerar chave privada assinar o token
            var privatekey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));

            //gerar assinatura digital do token
            var credentials = new SigningCredentials(privatekey, SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiracao
            var experation = DateTime.UtcNow.AddMinutes(10);

            //gerar o token
            JwtSecurityToken token = new JwtSecurityToken(
                //emissor
                issuer: _configuration["jwt:Issuer"],
                audience: _configuration["jwt:Audience"],
                claims: claims,
                expires: experation,
                signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = experation
            };
        }
    }
}
