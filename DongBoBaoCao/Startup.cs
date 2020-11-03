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

            //services.AddScoped<IBCService, BCService>();
            services.AddScoped<ICDDHService, CDDHService>();
            services.AddScoped<IDVCService, DVCService>();
            //services.AddScoped<IKNTCService, KNTCService>();
            services.AddScoped<IPAKNService, PAKNService>();
            //services.AddScoped<IQLCBService, QLCBService>();
            //services.AddScoped<IQLCHService, QLCHService>();
            //services.AddScoped<IQLVBService, QLVBService>();

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
            //RecurringJob.AddOrUpdate(() => Console.WriteLine("abc"), "*/3 6-20 * * 1-7", TimeZoneInfo.Local); // Mỗi 3 phút từ 6 - 22h, từ thứ 2 đến CN

            DuLieuChungService duLieuChungService = new DuLieuChungService(Configuration, httpService);
            RecurringJob.AddOrUpdate(() => duLieuChungService.Truncate(), "0 0 1 * *", TimeZoneInfo.Local);

            PAKNService pAKNService = new PAKNService(Configuration, httpService, loginService);
            RecurringJob.AddOrUpdate(() => pAKNService.CreateDanhSachDuLieu(), "0 0 1 * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => pAKNService.AddChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => pAKNService.RandomChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);

            DVCService dVCService = new DVCService(Configuration, httpService, loginService, dateTimeService);
            RecurringJob.AddOrUpdate(() => dVCService.CreateDanhSachDuLieu(), "0 0 1 * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => dVCService.AddChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => dVCService.RandomChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);

            //KNTCService kNTCService = new KNTCService(Configuration);
            //RecurringJob.AddOrUpdate(() => kNTCService.CreateDanhSachDuLieu(), "0 0 1 * *", TimeZoneInfo.Local);

            //QLCBService qLCBService = new QLCBService(Configuration);
            //RecurringJob.AddOrUpdate(() => qLCBService.CreateDanhSachDuLieu(), "0 0 1 * *", TimeZoneInfo.Local);

            //QLCHService qLCHService = new QLCHService(Configuration,);
            //RecurringJob.AddOrUpdate(() => qLCHService.CreateDanhSachDuLieu(), "0 0 1 * *", TimeZoneInfo.Local);

            //QLVBService qLVBService = new QLVBService(Configuration, commonService, dateTimeService, httpService);
            //RecurringJob.AddOrUpdate(() => qLVBService.AddChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate(() => qLVBService.AddChiTieuBaoCao1(), "0 0 1 * *", TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate(() => qLVBService.RandomChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);

            CDDHService cDDHService = new CDDHService(Configuration, httpService, loginService);
            RecurringJob.AddOrUpdate(() => cDDHService.CreateDanhSachDuLieu(), "0 0 1 * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => cDDHService.AddChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => cDDHService.RandomChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);

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
