using Sbp.Application.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DCVLicenseServer.Application
{
    public class CreatePLDto
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

        public string Remark { get; set; }

    }
}
