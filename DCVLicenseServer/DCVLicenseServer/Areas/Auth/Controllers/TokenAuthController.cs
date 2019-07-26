using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DCVLicenseServer.Core.Auth;
using DCVLicenseServer.Web.Core.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sbp.AspNetCore.AspNetCore.Mvc.Controllers;
using Sbp.Runtime.Security;

namespace DCVLicenseServer.Web.Areas.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenAuthController : SbpController
    {
        private readonly TokenAuthConfiguration _configuration;
        public TokenAuthController(TokenAuthConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 身份验证、获取token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {

            if (model.UserName!="admin"||model.Password!="dcvlicense")
            {
                return BadRequest();
            }

            var principal = new ClaimsPrincipal();

            //principal.Identities.First().AddClaim(new Claim(ClaimTypes.Name, model.UserName));
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.UserName));

            var accessToken = CreateAccessToken(CreateJwtClaims(identity));

            var res = new AuthenticateResultModel
            {
                AccessToken = accessToken,
                //EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                UserId = 1
            };

            return Ok(new
            {
                res
            });
        }


        [NonAction]
        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
        [NonAction]
        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        //[NonAction]
        //private string GetEncrpyedAccessToken(string accessToken)
        //{
        //    return SimpleStringCipher.Instance.Encrypt(accessToken, "fuckGC2019MK2ccc");
        //}
    }
}