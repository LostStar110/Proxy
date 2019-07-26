using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCVLicenseServer.Application;
using DCVLicenseServer.Application.ProLicense.Dto;
using DCVLicenseServer.Web.Core.GrantSystem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sbp.AspNetCore.AspNetCore.Mvc.Controllers;

namespace DCVLicenseServer.Web.Areas.License.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductLicenseController : AuthBaseController
    {
        private readonly ProductLicenseService _productLicenseService;
        public ProductLicenseController(ProductLicenseService productLicenseService)
        {
            _productLicenseService = productLicenseService;
        }

        /// <summary>
        /// 创建授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public IActionResult Create(CreatePLDto input)
        {
            var now = DateTime.Now;
            var clientIP = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(clientIP))
            {
                clientIP = HttpContext.Connection.RemoteIpAddress.ToString();
            }

            var result =  GrantManager.Inst.CreatePLGrantFile(input,now);

            _productLicenseService.CreateLicense(input, now,result, clientIP);

            return Ok(new
            {
                res= result.ZipFilePath
            });
        }


        /// <summary>
        ///  按项目名分页获取授权、
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageCount"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet("GetPageList/{index}/{pageCount}/{project?}")]
        public IActionResult GetPageList(int index,int pageCount,string project)
        {
            var list = new List<PLDto>();

            var pagination = _productLicenseService.GetPageList(index, pageCount, project,  list);

            return Ok(new {
                res=list,
                pagination
            });
        }


    }
}