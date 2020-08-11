using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace QuestionAndAnswer
{
    // TODO list:
    // 1. Версионность (Nick Chapsas)
    // 2. Mediatr + EventSourcing (INotification) (https://medium.com/@ducmeit/net-core-using-cqrs-pattern-with-mediatr-part-2-cc55763e83f0)
    // 3. AutoMapper
    // 4. SSL certificate
    
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}