using DCVLicenseServer.Core;
using Sbp.Application.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DCVLicenseServer.Application.APILicenses.Dto
{
    public class ALDto:EntityDto<int>
    {
        public string Project { get; set; }
        /// <summary>
        /// 索要者
        /// </summary>
        public string Claimer { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }

        public int TotalTime { get; set; }
        //创建时间
        public DateTime CreateTime { get; set; }

        //第三方应用id
        public string AppId { get; set; }

        //apilist
        public List<ProjectAPIInfo> APIList { get; set; }

        public string ZipFilePath { get; set; }
        //备注
        public string Remark { get; set; }

        public string CreateUserIp { get; set; }


    }
}
