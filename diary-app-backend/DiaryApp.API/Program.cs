using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace DiaryApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
             //.UseKestrel()              
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseIISIntegration()
             //.UseUrls("http://localhost:5001/")
             .Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
               /* .UseDefaultServiceProvider(options => options.ValidateScopes = false)*/;        
    }
}
