using Microsoft.Extensions.Configuration;
using MKProxyGatewayServer.Modles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MKProxyGatewayServer
{
    public static class MonitoringHealth
    {
        public static bool MainServerHealth = false;
        public static bool BakeServerHealth = false;

        public static string MainUrl = "";
        public static string BakeUrl = "";



        public static ServerConfig sc;

        public static async Task Start(IConfiguration configuration)
        {


            #region
            //string configStr = "";
            //using (var fs = new FileStream("serverconfig.json", FileMode.Open))
            //{
            //    configStr = MKUtils.IO.FileReader.ReadFile(fs);
            //}

            //sc = JsonConvert.DeserializeObject<ServerConfig>(configStr);


            //for (int i = 0; i <sc.ProxiedServerList.Count ; i++)
            //{
            //    var proxiedServer = sc.ProxiedServerList[i];

            //    HttpClient httpClient = new HttpClient();

            //    ThreadPool.QueueUserWorkItem(async (s) =>
            //    {
            //        if (string.IsNullOrEmpty(proxiedServer.URL))
            //        {
            //            MainServerHealth = false;
            //        }

            //        while (true)
            //        {
            //            //服务器健康状态
            //            var response = await httpClient.GetAsync(proxiedServer.URL + "/api/health");
            //            if (response.IsSuccessStatusCode)
            //            {
            //                var res = await response.Content.ReadAsAsync<string>();
            //                if (res.Equals("ok"))
            //                {
            //                    MainServerHealth = true;
            //                    Console.WriteLine($"{proxiedServer.Name}------正常"+DateTime.Now.ToString());
            //                }
            //                else
            //                {
            //                    MainServerHealth = false;
            //                    Console.WriteLine($"{proxiedServer.Name}------失去连接" + DateTime.Now.ToString());
            //                }
            //            }
            //            else
            //            {
            //                MainServerHealth = false;
            //                Console.WriteLine($"{proxiedServer.Name}------失去连接" + DateTime.Now.ToString());
            //            }


            //            Thread.Sleep(2000);
            //        }
            //    });
            //}

            #endregion



            //创建检测主服务器健康状态请求
            HttpClient httpClient_main = new HttpClient();

            MainUrl = configuration.GetSection("ProxyServer:MainServer").Value;
            if (string.IsNullOrEmpty(MainUrl))
            {
                MainServerHealth = false;
            }

            //创建检测备份服务器健康状态请求
            HttpClient httpClient_bake = new HttpClient();

            var BakeUrl = configuration.GetSection("ProxyServer:BakeServer").Value;
            if (string.IsNullOrEmpty(BakeUrl))
            {
                MainServerHealth = false;
            }


            ThreadPool.QueueUserWorkItem(async (s) =>
            {
               while (true)
               {
                   Console.WriteLine("-------检测服务器运行状态-------");

                   try
                   {
                        //检测主服务器
                        var response_main = await httpClient_main.GetAsync(MainUrl + "/api/health");


                       if (response_main.IsSuccessStatusCode )
                       {
                           var res = await response_main.Content.ReadAsAsync<string>();
                           if (res.Equals("ok"))
                           {
                               MainServerHealth = true;
                               Console.WriteLine("主服务器------正常");
                           }
                           else
                           {
                               MainServerHealth = false;
                               Console.WriteLine("主服务器----失去连接");
                           }
                       }
                       else
                       {
                           MainServerHealth = false;
                           Console.WriteLine("主服务器----失去连接");
                       }
                   }
                   catch (Exception e)
                   {
                        //Console.WriteLine("尝试连接主服务器出错："+e.ToString());
                        MainServerHealth = false;
                       Console.WriteLine("主服务器----失去连接");
                   }

                   try
                   {
                        //检测备份服务器
                        var response_bake = await httpClient_bake.GetAsync(BakeUrl + "/api/health");

                       if (response_bake.IsSuccessStatusCode)
                       {
                           var res = await response_bake.Content.ReadAsAsync<string>();
                           if (res.Equals("ok"))
                           {
                               BakeServerHealth = true;
                               Console.WriteLine("备份服务器----正常");
                           }
                           else
                           {
                               BakeServerHealth = false;
                               Console.WriteLine("备份服务器----失去连接");
                           }
                       }
                       else
                       {
                           BakeServerHealth = false;
                           Console.WriteLine("备份服务器----失去连接");
                       }

                   }
                   catch (Exception e)
                   {
                        //Console.WriteLine("尝试连接备份服务器出错：" + e.ToString());
                        BakeServerHealth = false;
                       Console.WriteLine("备份服务器----失去连接");
                   }
                   Thread.Sleep(1000);
               }
           });





        }


    }
}
