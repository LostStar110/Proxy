using DCVLicenseServer.Core;
using DCVLicenseServer.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;
using Sbp.Configuration.Startup;
using Sbp.EntityFrameworkCore.EntityFrameworkCore.Configuration;
using Sbp.EntityFrameworkCore.Mysql;
using Sbp.EntityFrameworkCore.Mysql.Configuration;
using Sbp.Modules;
using Sbp.Reflection.Extensions;

namespace DCVLicenseServer.EntityFrameworkCore
{
    [DependsOn(
        typeof(SbpEntityFrameworkCoreMysqlModule),
        typeof(CoreModule)
     )]
    public class EFCoreModule:SbpModule
    {

        public override void PreInitialize()
        {
            var startCfg = IocManager.Resolve<ISbpStartupConfiguration>();
            var efCfg = IocManager.Resolve<ISbpEfCoreConfiguration>();

            var conn = startCfg.DefaultNameOrConnectionString;
            efCfg.AddMysqlContext<MainDbContext>(conn);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(EFCoreModule).GetAssembly());
        }

        public override async void PostInitialize()
        {
            var context = IocManager.Resolve<MainDbContext>();

            await context.Database.MigrateAsync();

            await context.Database.EnsureCreatedAsync();

        }

    }
}
