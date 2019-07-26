using DCVLicenseServer.Application.ProLicense;
using DCVLicenseServer.Application.ProLicense.Dto;
using DCVLicenseServer.Core;
using DCVLicenseServer.Core.ProductLicense;
using Sbp.Application;
using Sbp.Domain.Repositories;
using Sbp.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCVLicenseServer.Application
{
    public class ProductLicenseService:ApplicationService
    {
        private readonly IRepository<ProductLicense> _plRepository;
        public ProductLicenseService(IRepository<ProductLicense> plRepository)
        {
            _plRepository = plRepository;
        }


        public void CreateLicense(CreatePLDto input,DateTime time, PLFileResult result,string clientIP)
        {

            var pl = ObjectMapper.Map<ProductLicense>(input);

            pl.TotalTime = pl.EndTime - pl.StartTime ;
            pl.CreateTime = time;
            pl.SystemFileName = result.SysFileName;
            pl.SystemFileContent = result.SysFileContent;
            pl.TimeFileContent = result.TimeFileContent;
            pl.TimeFileName = result.TimeFileName;
            pl.CreateUserIp = clientIP;
            pl.ZipFilePath = result.ZipFilePath;

            _plRepository.Insert(pl);

            _plRepository.SaveChanges();
        }


        public Pagination GetPageList(int index, int pageCount, string project,List<PLDto> dataList)
        {

            var query = _plRepository.GetAllList(p =>string.IsNullOrEmpty(project)?true: p.Project == project);
            if (query.Count()>0)
            {
                var sqlData = query.OrderByDescending(p => p.CreateTime).Skip((index - 1) * pageCount).Take(pageCount).ToList();

                dataList.AddRange(ObjectMapper.Map<List<PLDto>>(sqlData));
            }

            Pagination pagination = new Pagination(index, pageCount, query.Count());

            return pagination;

        }



    }
}
