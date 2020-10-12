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
using Microsoft.AspNetCore.Server.IISIntegration;
using DongBoBaoCao.Core.Services.VinhLong;

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

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services.AddAuthentication(IISDefaults.AuthenticationScheme);

            // Add services (Dependency injection)
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IDuLieuChungService, DuLieuChungService>();
            services.AddScoped<IDateTimeService, DateTimeService>();

            services.AddScoped<IBCService, BCService>();
            services.AddScoped<ICDDHService, CDDHService>();
            services.AddScoped<IDVCService, DVCService>();
            services.AddScoped<IKNTCService, KNTCService>();
            services.AddScoped<IPAKNService, PAKNService>();
            services.AddScoped<IQLCBService, QLCBService>();
            services.AddScoped<IQLCHService, QLCHService>();
            services.AddScoped<IQLVBService, QLVBService>();
            services.AddScoped<TruongHocService>();
            services.AddScoped<TinhThanhService>();
            services.AddScoped<QuanHuyenService>();
            services.AddScoped<DonViGiaoDucService>();
            services.AddScoped<PhuongXaService>();
            services.AddScoped<DanTocService>();
            services.AddScoped<TonGiaoService>();
            services.AddScoped<GiaoVienService>();
            services.AddScoped<HocSinhService>();
            services.AddScoped<LopHocService>();

            services.AddScoped<DVCHoSoService>();
            services.AddScoped<DVCLinhVucService>();
            services.AddScoped<DVCDonViService>();
            services.AddScoped<DVCThuTucHanhChinhService>();

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
        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env, ICommonService commonService, IHttpService httpService, IDateTimeService dateTimeService)
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

            PAKNService pAKNService = new PAKNService(Configuration, commonService);
            RecurringJob.AddOrUpdate(() => pAKNService.CreateDanhSachDuLieu(), "0 0 1 * *", TimeZoneInfo.Local);

            KNTCService kNTCService = new KNTCService(Configuration, commonService);
            RecurringJob.AddOrUpdate(() => kNTCService.CreateDanhSachDuLieu(), "0 0 1 * *", TimeZoneInfo.Local);

            QLCBService qLCBService = new QLCBService(Configuration, commonService);
            RecurringJob.AddOrUpdate(() => qLCBService.CreateDanhSachDuLieu(), "0 0 1 * *", TimeZoneInfo.Local);

            QLCHService qLCHService = new QLCHService(Configuration, commonService);
            RecurringJob.AddOrUpdate(() => qLCHService.CreateDanhSachDuLieu(), "0 0 1 * *", TimeZoneInfo.Local);

            QLVBService qLVBService = new QLVBService(Configuration, commonService, dateTimeService, httpService);
            RecurringJob.AddOrUpdate(() => qLVBService.AddChiTieuBaoCao(), "0 0 1 * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate(() => qLVBService.AddChiTieuBaoCao1(), "0 0 1 * *", TimeZoneInfo.Local);

            TruongHocService truongHocService = new TruongHocService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => truongHocService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            TinhThanhService tinhThanhService = new TinhThanhService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => tinhThanhService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            QuanHuyenService quanHuyenService = new QuanHuyenService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => quanHuyenService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            DonViGiaoDucService donViGiaoDucService = new DonViGiaoDucService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => donViGiaoDucService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            PhuongXaService phuongXaService = new PhuongXaService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => phuongXaService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            DVCHoSoService dVCHoSoService = new DVCHoSoService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => dVCHoSoService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            DVCLinhVucService dVCLinhVucService = new DVCLinhVucService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => dVCLinhVucService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            DVCDonViService dVCDonViService = new DVCDonViService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => dVCDonViService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            DVCThuTucHanhChinhService dVCThuTucHanhChinhService = new DVCThuTucHanhChinhService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => dVCThuTucHanhChinhService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            DanTocService danTocService = new DanTocService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => danTocService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            TonGiaoService tonGiaoService = new TonGiaoService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => tonGiaoService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            GiaoVienService giaoVienService = new GiaoVienService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => giaoVienService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            HocSinhService hocSinhService = new HocSinhService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => hocSinhService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

            LopHocService lopHocService = new LopHocService(httpService, Configuration);
            RecurringJob.AddOrUpdate(() => lopHocService.Create(), "0 0 1 * *", TimeZoneInfo.Local);

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
