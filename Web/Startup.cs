using Core.Services;
using Core.Services.DapperServices;
using Core.Services.Interfaces;
using Core.Services.Interfaces.DapperServices.Interfaces;
using DataLayer.Context;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WebMarkupMin.AspNetCore5;

namespace Web
{
    public class Startup
    {        
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.Configure<FormOptions>(option => option.MultipartBodyLengthLimit = 1073741824);
            _ = services.AddWebMarkupMin(options => { options.AllowMinificationInDevelopmentEnvironment = true; options.AllowCompressionInDevelopmentEnvironment = true; }).AddHtmlMinification(options => { options.MinificationSettings.RemoveRedundantAttributes = true; options.MinificationSettings.RemoveHttpProtocolFromAttributes = true; options.MinificationSettings.RemoveHttpsProtocolFromAttributes = true; }).AddHttpCompression();           _ = services.AddSession();
            _ = services.AddMvc(option => option.EnableEndpointRouting = true).AddControllersAsServices().AddRazorRuntimeCompilation();
            _ = services.AddControllersWithViews().AddControllersAsServices().AddRazorRuntimeCompilation(); 

            IMvcBuilder razorBuilder = services.AddRazorPages().AddRazorRuntimeCompilation();
            if (WebHostEnvironment.IsDevelopment())
            {
                _ = razorBuilder.AddRazorRuntimeCompilation();
            }
            _ = services.ConfigureApplicationCookie(options => { options.Cookie.SameSite = SameSiteMode.None; });
            _ = services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            _ = services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = true;

            });
            #region Authentication
            _ = services.AddAuthentication(options => { options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme; options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme; options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; }).AddCookie(options => { options.LoginPath = "/Login"; options.LogoutPath = "/Logout"; options.AccessDeniedPath = "/AccessDenied"; options.ExpireTimeSpan = TimeSpan.FromMinutes(43200); });
            #endregion
            #region tempkey
            string keysFolder = Path.Combine(WebHostEnvironment.ContentRootPath, "wwwroot/temp-keys");
            _ = services.AddDataProtection().SetApplicationName("Web").PersistKeysToFileSystem(new DirectoryInfo(keysFolder)).SetDefaultKeyLifetime(TimeSpan.FromDays(180));
            #endregion
            #region DatabaseContext            
            _ = services.AddDbContext<MyContext>(x => x.UseSqlServer(Configuration.GetConnectionString("MInsConnection")));
            #endregion DatabaseContext
            #region IOC   
            //services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            _ = services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            _ = services.AddScoped<ICompService, CompService>();
            _ = services.AddScoped<IUserService, UserService>();
            _ = services.AddScoped<IBlogService, BlogService>();
            _ = services.AddScoped<IThirdPartyService, ThirdPartyService>();
            _ = services.AddScoped<IVehicleGroupService, VehicleGroupService>();
            _ = services.AddScoped<ILifeInsService, LifeInsService>();
            _ = services.AddScoped<IPlanPaymentService, PlanPaymentService>();
            _ = services.AddScoped<IGenericInsService, GenericInsService>();
            _ = services.AddScoped<IThirdPartyDepperService, ThirdPartyDapperService>();
            _ = services.AddScoped<IFireInsService, FireInsService>();
            _ = services.AddScoped<ICarBodyService, CarBodyService>();
            _ = services.AddScoped<ILiabilityService, LiabilityService>();
            _ = services.AddScoped<ITravelService, TravelService>();
            #endregion IOC
            #region Encoder
            _ = services.AddSingleton(
                HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
                    UnicodeRanges.Arabic }));
            #endregion Encoder
            _ = services.AddElmah(options =>
            {
                options.Path = @"InsElmah";
                options.OnPermissionCheck = context => context.User.Identity.IsAuthenticated;

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _ = app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/404";
                    await next();
                }
            });
            _ = app.UseStatusCodePages();
            _ = app.UseStatusCodePagesWithRedirects("/404");
            _ = app.UseStaticFiles();
            _ = app.UseSession();
            _ = app.UseAuthentication();
            _ = app.UseRouting();
            _ = app.UseAuthorization();
            _ = app.UseWebMarkupMin();
            _ = app.UseElmah();
            _ = app.UseHttpsRedirection();

            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapAreaControllerRoute(
                    name: "AdminArea",
                    areaName: "UsersPanel",
                    pattern: "UsersPanel/{controller=Home}/{action=Index}/{id?}");

                _ = endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Generic}/{action=Index}/{id?}");

                _ = endpoints.MapRazorPages();


            });
        }

    }
}
