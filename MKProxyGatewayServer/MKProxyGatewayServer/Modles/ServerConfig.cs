using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKProxyGatewayServer.Modles
{
    public class ServerConfig
    {
        public List<ProxiedServer> ProxiedServerList { get; set; }
    }


    /// <summary>
    /// 被代理的服务器
    /// </summary>
    public class ProxiedServer
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
    }

}
