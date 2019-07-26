using DCVLicenseServer.Web.Core.Model;
using Microsoft.AspNetCore.Mvc;
using MKUtils.Security;
using Sbp.AspNetCore.AspNetCore.Mvc.Controllers;
using System.Text;

namespace DCVLicenseServer.Web.Areas.Utils.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MD5Controller : SbpController
    {
        /// <summary>
        /// MD5算法加密字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("EncryptString")]
        public IActionResult EncryptString([FromBody]DataString input)
        {
            var res = MD5Util.MD5Encrypt(input.Data, Encoding.UTF8);

            return Ok(new
            {
                res
            });
        }

    }
}