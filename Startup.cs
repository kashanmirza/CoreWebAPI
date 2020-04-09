using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreWebAPI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoreWebAPI.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using CoreWebAPI.Helpers;
using Microsoft.IdentityModel.Tokens;
using CoreWebAPI.Services;

namespace CoreWebAPI
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

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>();

            services.AddSwaggerGen(c => {
                //    x => {
                //    x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "", Version = "v1" });
                //}

                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = ".Net Core API",
                    Description = "Powerful API developed on ASP.NET Core",
                    TermsOfService = "None",
                });
        });


            services.AddCors();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>

              option.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = false,
                  ValidateAudience = false,
                  ValidateIssuerSigningKey = true,
                  ValidIssuer = "localhost:5001",
                  ValidAudience = "localhost:5001",
                  IssuerSigningKey = new SymmetricSecurityKey(key)
              }

            );
            

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };

            //});


            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
          //  services.AddScoped<IRoleService, RoleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //Swagger Configuration
            var _swaggerConfiguration = new SwaggerConfig();
            Configuration.GetSection(nameof(SwaggerConfig)).Bind(_swaggerConfiguration);

            app.UseSwagger(
            //    option =>
            //{
            //    option.RouteTemplate = _swaggerConfiguration.JsonRoute;
            //}
            );

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            }

                //option => {
                //option.SwaggerEndpoint( _swaggerConfiguration.UIEndpoint, _swaggerConfiguration.Description);
                //    }
                 );

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors(
             options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
         );


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseAuthentication();
        }
    }
}
