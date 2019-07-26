using Sbp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;


namespace DCVLicenseServer.Core.APILicenses
{
    public class APILicense:Entity<int>
    {
        [Required]
        public string Project { get; set; }
        /// <summary>
        /// 索要者
        /// </summary>
        [Required]
        public string Claimer { get; set; }
        [Required]
        public int StartTime { get; set; }
        [Required]
        public int EndTime { get; set; }

        public int TotalTime { get; set; }
        //创建时间
        public DateTime CreateTime { get; set; }

        //第三方应用id
        [Required]
        public string AppId { get; set; }

        //apilist
        public string APIList { get; set; }

        public string FileName { get; set; }
        public string FileContent { get; set; }
        public string ZipFilePath { get; set; }
        //备注
        public string Remark { get; set; }
        public string CreateUserIp { get; set; }
    }
}
