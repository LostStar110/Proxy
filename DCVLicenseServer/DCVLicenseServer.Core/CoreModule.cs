using Sbp.Modules;
using Sbp.Reflection.Extensions;

namespace DCVLicenseServer.Core
{

    public class CoreModule:SbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CoreModule).GetAssembly());
        }

    }
}
