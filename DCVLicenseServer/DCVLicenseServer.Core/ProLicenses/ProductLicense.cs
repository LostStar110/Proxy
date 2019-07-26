using Sbp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DCVLicenseServer.Core.ProductLicense
{
    public class ProductLicense:Entity<int>
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

        /// <summary>
        /// 机器码
        /// </summary>
        [Required]
        public string MachineCode { get; set; }
        /// <summary>
        /// 独立设备数量上限
        /// </summary>
        [Required]
        public int DeviceNumber { get; set; }
        /// <summary>
        /// 系统授权文件名称
        /// </summary>
        public string SystemFileName { get; set; }
        public string SystemFileContent { get; set; }
        /// <summary>
        /// 时间授权文件名称
        /// </summary>
        public string TimeFileName { get; set; }
        public string TimeFileContent { get; set; }
        public string CreateUserIp { get; set; }
        public string Remark { get; set; }
        public string ZipFilePath { get; set; }
    }
}
