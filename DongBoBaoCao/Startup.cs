using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.SqlServer;
using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.Services;
using DongBoBaoCao.Interfaces;

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
            services.AddScoped<IDuLieuChungService, DuLieuChungService>();
            services.AddScoped<IDateTimeService, DateTimeService>();

            services.AddScoped<ICDDHService, CDDHService>();
            services.AddScoped<IDVCService, DVCService>();
            services.AddScoped<IPAKNService, PAKNService>();

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
            PAKNService pAKNService = new PAKNService(Configuration, httpService, loginService);
            RecurringJob.AddOrUpdate(() => pAKNService.CreateDanhSachDuLieu(), "0 0 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => pAKNService.AddChiTieuBaoCao(), "0 1 * * *", TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate(() => pAKNService.RandomChiTieuBaoCao(), "0 0 * * *", TimeZoneInfo.Local);

            DVCService dVCService = new DVCService(Configuration, httpService, loginService, dateTimeService);
            RecurringJob.AddOrUpdate(() => dVCService.CreateDanhSachDuLieu(), "0 0 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => dVCService.AddChiTieuBaoCao(), "0 1 * * *", TimeZoneInfo.Local);

            CDDHService cDDHService = new CDDHService(Configuration, httpService, loginService);
            RecurringJob.AddOrUpdate(() => cDDHService.CreateDanhSachDuLieu(), "0 0 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => cDDHService.AddChiTieuBaoCao(), "0 1 * * *", TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate(() => cDDHService.RandomChiTieuBaoCao(), "0 0 * * *", TimeZoneInfo.Local);

            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

            app.UseRouting();

            //app.UseAuthorization();

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
