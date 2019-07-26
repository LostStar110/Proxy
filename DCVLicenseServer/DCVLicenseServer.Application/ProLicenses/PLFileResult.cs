using System;
using System.Collections.Generic;
using System.Text;

namespace DCVLicenseServer.Application.ProLicense
{
    public class PLFileResult
    {
        public string SysFileName { get; set; }

        public string SysFileContent { get; set; }

        public string TimeFileName { get; set; }

        public string TimeFileContent { get; set; }

        public string ZipFilePath { get; set; }
    }
    public class ALFileResult
    {
        public string FileName { get; set; }

        public string FileContent { get; set; }

        public string ZipFilePath { get; set; }
    }
}
