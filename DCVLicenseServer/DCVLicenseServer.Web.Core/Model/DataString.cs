using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DCVLicenseServer.Web.Core.Model
{
    public class DataString
    {
        [Required]
        public string Data { get; set; }
    }

    public class DesDataString
    {
        [Required]
        public string Data { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(8)]
        public string Key { get; set; }
    }
}
