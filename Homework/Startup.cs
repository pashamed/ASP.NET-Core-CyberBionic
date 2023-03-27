using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using Homework.Models;
using Homework.Services;
using Homework.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Homework
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // ��� �������� ������ � ������ � ��� ������������� ����������� (IMemoryCache)
            services.AddMemoryCache();
            // ���������� � ������ ������
            services.AddSession(options =>
            {
                options.Cookie.Name = ".MyAppV.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
                options.Cookie.IsEssential = true;
            });

            //services.AddSession();

            //���������� service
            services.AddTransient<CalcService>(); 

            services.AddTransient<DayService>();
            services.AddTransient<MonthsService>();

            services.AddSingleton<Visitors>();
            services.AddSingleton<WriterAndReadService>();

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, Services.MailService>();
            services.AddControllers();

            // ����������� ���������� ��������. ��� ������� ����� ����������� ��� ������� ����������� � ������ �������� � ����������.
            services.AddControllersWithViews()
                .AddMvcOptions(
                options =>
                {
                    options.Filters.Add<CountUsersAttribute>(); 
                    options.Filters.Add<LogAction>(); 
                    
                })
                ;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Use(async (context, next) =>
            //{
            //    var visitors = context.RequestServices.GetService<Visitors>();
            //    visitors.AddSession(context.Request.Cookies[".MyApp.Session"]);
            //    await next();
            //});

            //��� ������ � �������
            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("main",
                    pattern: "{title?}",
                    defaults: new
                    {
                        controller = "Home",
                        action = "index"
                    });

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller}/{action}/{id?}");

                endpoints.MapControllerRoute(
                      name: "Calculate",
                      pattern: "{controller}/{action}/{a}/{b}"
                  );
            });
        }
    }
}
