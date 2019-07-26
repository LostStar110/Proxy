using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MKProxyGatewayServer.Modles;
using Newtonsoft.Json;

namespace MKProxyGatewayServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //> 配置跨域
            //services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    //.WithOrigins("http://localhost:8001", "http://localhost:5000")
                    );
            });
            services.AddSingleton(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //var url = Configuration.GetSection("ProxyServer:MainServer");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCors("CorsPolicy");

            app.UseMiddleware<ReverseProxyMiddleware>();

            app.UseMvc();
          

                MonitoringHealth.Start(Configuration);


            //string password = "12345";
            string password = "12345";

            string name1 = "1";
            string name2 = "2";

            if (name1 == name2)
            {
                Console.WriteLine();
            }
            if (string.IsNullOrEmpty(name1))
            {
                Console.WriteLine();
            }
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

        }
    }


    public struct ProxyServerList
    {
        public List<string> list { get; set; }
    }
}
