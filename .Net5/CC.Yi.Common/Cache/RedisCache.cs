using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Yi.Common.Cache
{
    public class RedisCache : ICacheWriter
    {
        
        public static void RedisRegist( IConfiguration configuration)
        {
            ip = configuration["redis:ip"];
            port=int.Parse(configuration["redis:port"]);
            pwd = configuration["redis:pwd"];
        }

        private RedisClient client;
        private static string ip ;

        private static int port;
        private static string pwd;
        public RedisCache()
        {
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public bool AddCache<T>(string key, T value, TimeSpan expDate)
        {
            try
            {
                using (client = new RedisClient(ip, port, pwd))
                {
                    return client.Add<T>(key, value, expDate);
                }
            }
            catch
            {

                return false;
            }
        }

        public bool AddCache<T>(string key, T value)
        {
            return client.Add<T>(key, value);
        }

        public bool RemoveCache(string key)
        {
            return client.Remove(key);
        }

        public T GetCache<T>(string key)
        {
            try
            {
                using (client = new RedisClient(ip, port, pwd))
                {
                    return client.Get<T>(key);
                }
            }
            catch
            {
                object p = new object();
                return (T)p;
            }
        }



        public bool SetCache<T>(string key, T value, TimeSpan expDate)
        {
            try
            {
                using (client = new RedisClient(ip, port, pwd))
                {
                    return client.Set<T>(key, value, expDate);
                }
            }
            catch
            {
                return false;
            }
        }

        public bool SetCache<T>(string key, T value)
        {
            try
            {
                using (client = new RedisClient(ip, port, pwd))
                {
                    return client.Set<T>(key, value);
                }
            }
            catch
            {
                object p = new object();
                return false;
            }

        }
    }
}
