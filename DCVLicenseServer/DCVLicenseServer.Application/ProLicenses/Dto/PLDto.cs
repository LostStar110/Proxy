using Sbp.Application.Dto;
using Sbp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DCVLicenseServer.Application.ProLicense.Dto
{
    public class PLDto:EntityDto<int>
    {
        public string Project { get; set; }
        /// <summary>
        /// 索要者
        /// </summary>
        public string Claimer { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        //创建时间
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 机器码
        /// </summary>
        public string MachineCode { get; set; }
        /// <summary>
        /// 独立设备数量上限
        /// </summary>
        public int DeviceNumber { get; set; }
        public string CreateUserIp { get; set; }
        public string Remark { get; set; }
        public string ZipFilePath { get; set; }
    }
}
