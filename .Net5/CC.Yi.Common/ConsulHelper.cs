//using Consul;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace CC.Yi.Common
//{
//    public static class ConsulHelper
//    {
//        public static void ConsulRegist(this IConfiguration configuration)
//        {
//            ConsulClient client = new ConsulClient(c =>
//            {
//                c.Address = new Uri("http://localhost:8500/");
//                c.Datacenter = "dcl";
//            });

            
//            string ip = configuration["ip"];
            

//            int.TryParse(configuration["port"],out int port);

//            int weight = string.IsNullOrWhiteSpace(configuration["weight"]) ? 1 : int.Parse(configuration["weight"]);

//            client.Agent.ServiceRegister(new AgentServiceRegistration()
//            {
//                //ID = "service" + Guid.NewGuid(),
//                ID = "service" + port,
//                Name = "CC.yi.bbs",
//                Address = ip,
//                Port = port,
//                Tags = new string[] { weight.ToString() },
//                Check = new AgentServiceCheck()
//                {
//                    Interval = TimeSpan.FromSeconds(12),
//                    HTTP = $"http://{ip}:{port}/Health/Index",
//                    Timeout=TimeSpan.FromSeconds(5),
//                    DeregisterCriticalServiceAfter=TimeSpan.FromSeconds(60)
//                }
//            });

//            Console.WriteLine("");
//        }
//    }
//}
