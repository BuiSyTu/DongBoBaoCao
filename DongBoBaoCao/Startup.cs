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
            services.AddScoped<ICommonService, CommonService>();

            services.AddScoped<IBCService, BCService>();
            services.AddScoped<ICDDHService, CDDHService>();
            services.AddScoped<IDVCService, DVCService>();
            services.AddScoped<IKNTCService, KNTCService>();
            services.AddScoped<IPAKNService, PAKNService>();
            services.AddScoped<IQLCBService, QLCBService>();
            services.AddScoped<IQLCHService, QLCHService>();
            services.AddScoped<IQLVBService, QLVBService>();

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
        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env, ICommonService commonService)
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
            //RecurringJob.AddOrUpdate(() => Console.WriteLine("abc"), "*/3 6-20 * * 1-7", TimeZoneInfo.Local); // Mỗi 3 phút từ 6 - 22h, từ thứ 2 đến CN

            PAKNService pAKNService = new PAKNService(Configuration, commonService);
            RecurringJob.AddOrUpdate(() => pAKNService.CreateDanhSachDuLieu(), "*/1 6-20 * * 1-7", TimeZoneInfo.Local); //*/1 6-20 * * 1-7
            //RecurringJob.AddOrUpdate(() => pAKNService.CreateDanhSachDuLieuTrongNgay(), "*/1 6-20 * * 1-7", TimeZoneInfo.Local);

            KNTCService kNTCService = new KNTCService(Configuration, commonService);
            RecurringJob.AddOrUpdate(() => kNTCService.CreateDanhSachDuLieu(), "*/1 6-20 * * 1-7", TimeZoneInfo.Local); //*/1 6-20 * * 1-7

            QLCBService qLCBService = new QLCBService(Configuration, commonService);
            RecurringJob.AddOrUpdate(() => qLCBService.CreateDanhSachDuLieu(), "*/1 6-20 * * 1-7", TimeZoneInfo.Local); //*/1 6-20 * * 1-7

            QLCHService qLCHService = new QLCHService(Configuration, commonService);
            RecurringJob.AddOrUpdate(() => qLCHService.CreateDanhSachDuLieu(), "*/1 6-20 * * 1-7", TimeZoneInfo.Local); //*/1 6-20 * * 1-7



            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

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
