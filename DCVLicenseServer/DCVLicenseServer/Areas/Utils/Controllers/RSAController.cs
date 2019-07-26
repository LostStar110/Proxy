using DCVLicenseServer.Web.Core;
using Microsoft.AspNetCore.Mvc;
using MKUtils.Security;
using Sbp.AspNetCore.AspNetCore.Mvc.Controllers;

namespace DCVLicenseServer.Web.Areas.Utils.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RSAController : SbpController
    {

        /// <summary>
        /// 生成RSA公钥和密钥
        /// </summary>
        /// <returns></returns>
        [HttpGet("CreateRSAKeys")]
        public IActionResult CreateRSAKeys()
        {
            var res = RSAUtil.GetRASKey();

            return Ok(new
            {
                res
            });
        }

        /// <summary>
        /// RSA算法根据公钥加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EncryptString")]
        public IActionResult EncryptString([FromBody]RSAEncryptString input)
        {
            var res = RSAUtil.EncryptString(input.Data, input.PublicKey);

            return Ok(new
            {
                res
            });
        }

        /// <summary>
        /// RSA算法根据私钥解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DecryptString")]
        public IActionResult DecryptString([FromBody]RSADecryptString input)
        {
            var res = RSAUtil.DecryptString(input.Data, input.PrivateKey);

            return Ok(new
            {
                res
            });
        }





    }
}