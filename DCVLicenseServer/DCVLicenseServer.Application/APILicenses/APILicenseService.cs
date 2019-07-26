using DCVLicenseServer.Application.APILicenses.Dto;
using DCVLicenseServer.Application.ProLicense;
using DCVLicenseServer.Core;
using DCVLicenseServer.Core.APILicenses;
using Newtonsoft.Json;
using Sbp.Application;
using Sbp.Domain.Repositories;
using Sbp.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCVLicenseServer.Application.APILicenses
{
    public class APILicenseService:ApplicationService
    {
        private readonly IRepository<APILicense> _repository;

        public APILicenseService(IRepository<APILicense> repository)
        {
            _repository = repository;
        }

        public void CreateLicense(CreateALDto input, DateTime time, ALFileResult result, string clientIP)
        {

            var al = ObjectMapper.Map<APILicense>(input);

            al.TotalTime = al.StartTime = al.EndTime;
            al.CreateTime = time;
            al.FileName = result.FileName;
            al.FileContent = result.FileContent;
            al.CreateUserIp = clientIP;
            al.ZipFilePath = result.ZipFilePath;
            al.AppId = input.AppId;


            al.APIList = JsonConvert.SerializeObject(input.APIList);

            _repository.Insert(al);

            _repository.SaveChanges();
        }

        public Pagination GetPageList(int index, int pageCount, string project, List<ALDto> dataList)
        {

            var query = _repository.GetAllList(p => string.IsNullOrEmpty(project) ? true : p.Project == project);

            if (query.Count() > 0)
            {
                var sqlData = query.OrderBy(p => p.CreateTime).Skip((index - 1) * pageCount).Take(pageCount).ToList();

                dataList.AddRange(ObjectMapper.Map<List<ALDto>>(sqlData));
            }

            Pagination pagination = new Pagination(index, pageCount, query.Count());

            return pagination;

        }


    }
}
