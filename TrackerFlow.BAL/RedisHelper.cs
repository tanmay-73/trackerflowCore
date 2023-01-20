using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerFlow.BAL
{
    public class RedisHelper : Helper
    {
        public void cntincrement()
        {
            RedisEndpoint redisEndpoint = new RedisEndpoint("localhost", 6379);
            using (var client = new RedisClient(redisEndpoint))
            {
                client.Increment("notification_Counter", 1).ToString();
            }
        }
        public string cntshow()
        {
            return "";
            
            //RedisEndpoint redisEndpoint = new RedisEndpoint("localhost", 6379);
            //using (var client = new RedisClient(redisEndpoint))
            //{
                //return client.Increment("notification_Counter", 0).ToString();                                
            //}
        }
        public string resetcnt()
        {
            RedisEndpoint redisEndpoint = new RedisEndpoint("localhost", 6379);
            using (var client = new RedisClient(redisEndpoint))
            {
                uint a = Convert.ToUInt32(client.Increment("notification_Counter", 0).ToString());
                if(a!=0)
                {
                    return client.Decrement("notification_Counter", 1).ToString();
                }
                return a.ToString();
            }
        }
    }
}
