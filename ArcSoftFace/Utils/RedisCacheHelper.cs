using System;
using System.Configuration;
using ServiceStack.Redis;
using StackExchange.Redis;

namespace ArcSoftFace.Utils
{
    //没用到
    public class RedisCacheHelper
    {
        static RedisCacheHelper()
        {
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379"))
            {

                IDatabase db = redis.GetDatabase();
                db.StringSet("guozheng", "hahaha");
                var age = db.StringGet("guozheng");
            }
        }
    }
        
}
