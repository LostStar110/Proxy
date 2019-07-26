using DCVLicenseServer.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sbp.AspNetCore.AspNetCore.Mvc.Controllers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DCVLicenseServer.Web.Areas.License.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : AuthBaseController
    {
        private readonly ProjectService _projectService;
        private readonly IHttpClientFactory _httpClientFactory;
        public ProjectController(ProjectService projectService, IHttpClientFactory httpClientFactory)
        {
            _projectService = projectService;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 获取所有项目
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var res = _projectService.GetAll();

            return Ok(new {
                res
            });
        }


        /// <summary>
        /// 添加项目
        /// </summary>
        /// <returns></returns>
        [HttpPost("Create")]
        public IActionResult Create([FromBody]ProjectDto input)
        {
            var res = _projectService.CreateProject(input);
            if (res)
                return Ok();
            else
                return BadRequest();
        }


        /// <summary>
        /// 删除项目
        /// </summary>
        /// <returns></returns>
        [HttpDelete("Delete/{name}")]
        public IActionResult Delete(string name)
        {
            _projectService.DeleteProject(name);

            return Ok();
        }

        /// <summary>
        /// 获取指定项目的OpenAPI列表 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("GetOpenAPI/{name}")]
        public async Task<IActionResult> GetOpenAPI(string name)
        {
            var pro = _projectService.Get(name);
            if (pro==null)
                return NotFound();

            var client = _httpClientFactory.CreateClient();

            client.BaseAddress = new Uri(pro.Url);
            var response = await client.GetAsync("/api/OpenApi/GetAPIs");


            var message = response.Content.ReadAsStringAsync().Result.ToString();
            if (string.IsNullOrEmpty(message))
            {
                return NotFound();
            }

            //var data = JsonConvert.DeserializeObject<JObject>(message);

            //var apiList = JsonConvert.DeserializeObject<List<ProjectAPIInfo>>(data["res"].ToString());

            //string password = "12345";
            string password = "12345";

            string name1 = "1";
            string name2 = "2";

            if (name1==name2)
            {
                Console.WriteLine();
            }
            if (string.IsNullOrEmpty(name1))
            {
                Console.WriteLine();
            }
            try
            {

            }
            catch (Exception)
            {

                throw;
            }


            return Content(message);
        }



    }
}