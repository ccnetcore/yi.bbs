using Autofac.Extensions.DependencyInjection;
using CC.Yi.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Yi.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //���һ�����ݿ⣬���޸��������ݿ�������ļ�
            //���ģ���࣬ʹ��Add-Migration xxxǨ�ƣ���ʹ��Update-Database�������ݿ�
            //��T4Model���ģ������һ��ת������T4
            //���������캯����������ע��ֱ��ʹ��

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("��������Yi���ܡ�����������");
                var host = CreateHostBuilder(args).Build();
                host.Run();
                logger.Info("Yi���������ɹ���");
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }




        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    webBuilder.UseUrls("http://*:19000").UseStartup<Startup>();
                }).UseServiceProviderFactory(new AutofacServiceProviderFactory())
                 .ConfigureLogging(logging =>
                 {
                     // logging.ClearProviders(); // ���������������п���̨�����
                     logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                 }).UseNLog();//����nlog��־���

    }
}
