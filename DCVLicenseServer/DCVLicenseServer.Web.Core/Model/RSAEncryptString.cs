using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DCVLicenseServer.Web.Core
{
    public class RSAEncryptString
    {
        [Required]
        public string PublicKey { get; set; }
        [Required]
        public string Data { get; set; }
    }

    public class RSADecryptString
    {
        [Required]
        public string PrivateKey { get; set; }
        [Required]
        public string Data { get; set; }
    }
}
