using DCVLicenseServer.Web.Core.Model;
using Microsoft.AspNetCore.Mvc;
using MKUtils.Security;
using Sbp.AspNetCore.AspNetCore.Mvc.Controllers;

namespace DCVLicenseServer.Web.Areas.Utils.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DESController : SbpController
    {

        /// <summary>
        /// Des加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EncryptString")]
        public IActionResult EncryptString([FromBody]DesDataString input)
        {
            var res = DESUtil.EncryptString(input.Data, input.Key);

            return Ok(new
            {
                res
            });
        }

        /// <summary>
        /// Des解密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DecryptString")]
        public IActionResult DecryptString([FromBody]DesDataString input)
        {
            var res = DESUtil.DecryptString(input.Data, input.Key);

            return Ok(new
            {
                res
            });
        }
    }
}