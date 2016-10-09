using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

using Extensions;

namespace RedisAndDotNet
{
    class State
    {
        public string Value { get; set; }
    }

    class Log
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public DateTime LogDate { get; set; }
    }

    class Program
    {
        static void Main()
        {
            var redis = new RedisClient();
            using (redis)
            {
                var redisTodos = redis.As<Log>();
                redisTodos.Store(new Log()
                {
                    Id = redisTodos.GetNextSequence(),
                    Text = "Log crated on " + DateTime.Now,
                    LogDate = DateTime.Now
                });

                redisTodos.GetAll()
                          .Select(l => string.Format("[{0}] {1}", l.LogDate, l.Text))
                          .Print();

            }
        }
    }
}
