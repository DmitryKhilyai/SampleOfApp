using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPI.Authentication;
using WebAPI.Authentication.JWE;
using WebAPI.Authentication.JWS;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [AllowAnonymous]
        public ActionResult<string> Post(AuthenticationRequest authRequest,
            [FromServices] IJwtSigningEncodingKey signingEncodingKey,
            [FromServices] IJwtEncryptingEncodingKey encryptingEncodingKey)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, authRequest.Name)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(
                issuer: "SampleOfWebAPI",
                audience: "WebAPI",
                subject: new ClaimsIdentity(claims),
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                issuedAt: DateTime.Now,
                signingCredentials: new SigningCredentials(
                    signingEncodingKey.GetKey(),
                    signingEncodingKey.SigningAlgorithm),
                encryptingCredentials: new EncryptingCredentials(
                    encryptingEncodingKey.GetKey(),
                    encryptingEncodingKey.SigningAlgorithm,
                    encryptingEncodingKey.EncryptingAlgorithm));

            string jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}