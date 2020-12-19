using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.SqlServer;
using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Services;
using DongBoBaoCao.Core.Services;
using Microsoft.AspNetCore.Server.IISIntegration;

namespace DongBoBaoCao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Add services (Dependency injection)
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IDateTimeService, DateTimeService>();

            services.AddScoped<CDDHService>();
            services.AddScoped<DVCService>();
            services.AddScoped<PAKNService>();

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs,
            IWebHostEnvironment env, IHttpService httpService,
            IDateTimeService dateTimeService, ILoginService loginService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseHangfireDashboard();

            // Hangfire jobs
            PAKNService pAKNService = new PAKNService(Configuration, httpService, loginService, dateTimeService);
            RecurringJob.AddOrUpdate(() => pAKNService.CreateDanhSachDuLieu(), "0 0 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => pAKNService.AddChiTieuBaoCao(), "10 0 * * *", TimeZoneInfo.Local);

            DVCService dVCService = new DVCService(Configuration, httpService, loginService, dateTimeService);
            RecurringJob.AddOrUpdate(() => dVCService.CreateDanhSachDuLieuByDay(), "40 0 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => dVCService.AddChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);

            CDDHService cDDHService = new CDDHService(Configuration, httpService, loginService, dateTimeService);
            RecurringJob.AddOrUpdate(() => cDDHService.CreateDanhSachDuLieu(), "5 0 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => cDDHService.AddChiTieuBaoCao(), "20 0 * * *", TimeZoneInfo.Local);

            LogInfoService logInfoService = new LogInfoService(Configuration, httpService);
            RecurringJob.AddOrUpdate(() => logInfoService.AddChiTieuBaoCao(), "30 0 * * *", TimeZoneInfo.Local);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
