using DCVLicenseServer.Application;
using DCVLicenseServer.Web.Core.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sbp.AspNetCore.AspNetCore;
using Sbp.Modules;
using Sbp.Reflection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCVLicenseServer.Web
{
    [DependsOn(
       typeof(SbpAspNetCoreModule),
        typeof(ApplicationModule)
       )]
    public class AppModule:SbpModule
    {

        private readonly IConfiguration _configuration;

        public AppModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AppModule).GetAssembly());
            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();

            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _configuration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _configuration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }
    }
}
