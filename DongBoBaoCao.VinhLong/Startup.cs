using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.SqlServer;
using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.Services;
using DongBoBaoCao.VinhLong.Interfaces;
using DongBoBaoCao.VinhLong.Services;

namespace DongBoBaoCao.VinhLong
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

            services.AddScoped<IDanTocService, DanTocService>();
            services.AddScoped<IDonViGiaoDucService, DonViGiaoDucService>();
            services.AddScoped<IDVCDonViService, DVCDonViService>();
            services.AddScoped<IDVCHoSoService, DVCHoSoService>();
            services.AddScoped<IDVCLinhVucService, DVCLinhVucService>();
            services.AddScoped<IDVCThuTucHanhChinhService, DVCThuTucHanhChinhService>();
            services.AddScoped<IGiaoVienService, GiaoVienService>();
            services.AddScoped<IHocSinhService, HocSinhService>();
            services.AddScoped<ILopHocService, LopHocService>();
            services.AddScoped<IPhuongXaService, PhuongXaService>();
            services.AddScoped<IQuanHuyenService, QuanHuyenService>();
            services.AddScoped<ITinhThanhService, TinhThanhService>();
            services.AddScoped<ITonGiaoService, TonGiaoService>();
            services.AddScoped<ITruongHocService, TruongHocService>();
            services.AddScoped<HCCDonViService>();

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
        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env, IHttpService httpService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseHangfireDashboard();
            backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

            //TruongHocService truongHocService = new TruongHocService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => truongHocService.DeleteAndCreateNew(), "0 0 1 * *", TimeZoneInfo.Local);

            //TinhThanhService tinhThanhService = new TinhThanhService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => tinhThanhService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            //QuanHuyenService quanHuyenService = new QuanHuyenService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => quanHuyenService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            //DonViGiaoDucService donViGiaoDucService = new DonViGiaoDucService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => donViGiaoDucService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            //PhuongXaService phuongXaService = new PhuongXaService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => phuongXaService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            DVCHoSoService dVCHoSoService = new DVCHoSoService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => dVCHoSoService.Create(), "0 0 1 * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => dVCHoSoService.ThemChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);

            //DVCLinhVucService dVCLinhVucService = new DVCLinhVucService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => dVCLinhVucService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            //DVCDonViService dVCDonViService = new DVCDonViService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => dVCDonViService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            //DVCThuTucHanhChinhService dVCThuTucHanhChinhService = new DVCThuTucHanhChinhService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => dVCThuTucHanhChinhService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            //DanTocService danTocService = new DanTocService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => danTocService.DeleteAndCreateNew(), "0 0 1 * *", TimeZoneInfo.Local);

            //TonGiaoService tonGiaoService = new TonGiaoService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => tonGiaoService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            //GiaoVienService giaoVienService = new GiaoVienService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => giaoVienService.DeleteAndCreateNew(), "0 0 1 * *", TimeZoneInfo.Local);

            //HocSinhService hocSinhService = new HocSinhService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => hocSinhService.DeleteAndCreateNew(), "0 0 1 * *", TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate(() => hocSinhService.AddChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate(() => hocSinhService.AddChiTieuBaoCao1(), "0 0 1 * *", TimeZoneInfo.Local);

            //LopHocService lopHocService = new LopHocService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => lopHocService.DeleteAndCreateNew(), "0 0 1 * *", TimeZoneInfo.Local);

            //HCCDonViService hCCDonViService = new HCCDonViService(httpService, Configuration);
            //RecurringJob.AddOrUpdate(() => hCCDonViService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

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
