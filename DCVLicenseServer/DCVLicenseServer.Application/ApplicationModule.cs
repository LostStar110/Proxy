using AutoMapper;
using DCVLicenseServer.Application.APILicenses.Dto;
using DCVLicenseServer.Application.ProLicense.Dto;
using DCVLicenseServer.Core;
using DCVLicenseServer.Core.APILicenses;
using DCVLicenseServer.Core.ProductLicense;
using DCVLicenseServer.EntityFrameworkCore;
using Newtonsoft.Json;
using Sbp.AutoMapper;
using Sbp.AutoMapper.Configure;
using Sbp.Modules;
using Sbp.Reflection.Extensions;
using System.Collections.Generic;

namespace DCVLicenseServer.Application
{
    [DependsOn(
        typeof(SbpAutoMapperModule),
        typeof(CoreModule),
        typeof(EFCoreModule)
      )]
    public class ApplicationModule:SbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.SbpAutoMapper().Configurators.Add(InitializeMapperCfg);
        }


        public override void Initialize()
        {

            var thisAssembly = typeof(ApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
        private void InitializeMapperCfg(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CreatePLDto, ProductLicense>();
            cfg.CreateMap<ProjectDto, Project>();
            cfg.CreateMap<CreateALDto, APILicense>();
            cfg.CreateMap<APILicense, ALDto>().ForMember(dest => dest.APIList, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<List<ProjectAPIInfo>>(src.APIList)));

            //...
        }


    }
}
