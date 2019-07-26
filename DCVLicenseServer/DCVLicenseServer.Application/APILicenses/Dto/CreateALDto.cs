using DCVLicenseServer.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DCVLicenseServer.Application.APILicenses.Dto
{
    public class CreateALDto
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

        //第三方应用id
        [Required]
        public string AppId { get; set; }
        //apilist
        [Required]
        public List<ProjectAPIInfo> APIList { get; set; }
        //备注
        public string Remark { get; set; }
    }
}
