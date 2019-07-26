using DCVLicenseServer.Web.Core.Model;
using Microsoft.AspNetCore.Mvc;
using MKUtils.Security;
using Sbp.AspNetCore.AspNetCore.Mvc.Controllers;

namespace DCVLicenseServer.Web.Areas.Utils.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Base64Controller : SbpController
    {
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EncryptString")]
        public IActionResult EncryptString([FromBody]DataString input)
        {
            var res = Base64Util.EncryptString(input.Data);

            return Ok(new
            {
                res
            });
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DecryptString")]
        public IActionResult DecryptString([FromBody]DataString input)
        {
            var res = Base64Util.DecryptString(input.Data);

            return Ok(new
            {
                res
            });
        }



    }
}