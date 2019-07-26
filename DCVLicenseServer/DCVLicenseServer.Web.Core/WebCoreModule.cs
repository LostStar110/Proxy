using Sbp.Modules;
using Sbp.Reflection.Extensions;
using System;

namespace DCVLicenseServer.Web.Core
{

    public class WebCoreModule:SbpModule
    {
        public override void Initialize()
        {

            IocManager.RegisterAssemblyByConvention(typeof(WebCoreModule).GetAssembly());

        }

    }
}
