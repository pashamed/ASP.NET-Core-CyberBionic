using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homework.Models;


namespace Homework
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 //���������� ������ .json
                 .ConfigureAppConfiguration(config =>
                 {
                     config.AddJsonFile("appsettings.Task-5.json", optional: true)  
                     .AddJsonFile("appsettings.Task-1.json", optional: true, reloadOnChange: true)
                     .AddJsonFile("appsettings.Test.json", optional: true, reloadOnChange: true);
                 })
                // � ������ Logging ����� appsettigns.json ���������� ��������� ������� �����������
                .ConfigureLogging((context, builder) =>
                {
                    // ������ ��� ���������� � ������ CreateDefaultBuilder 
                    builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    // NuGet Package NetEscapades.Extensions.Logging.RollingFile
                    builder.AddFile(); 
                })
                //���������� Seq
                .ConfigureLogging(builder => builder.AddSeq())

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
