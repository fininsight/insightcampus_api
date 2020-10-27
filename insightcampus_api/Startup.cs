using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace insightcampus_api
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
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<DataContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ClassInterface, ClassRepository>();
            services.AddScoped<CategoryInterface, CategoryRepository>();
            services.AddScoped<RoleInterface, RoleRepository>();
            services.AddScoped<UserInterface, UserRepository>();
            services.AddScoped<RoleUserInterface, RoleUserRepository>();
            services.AddScoped<CodeInterface, CodeRepository>();
            services.AddScoped<CodegroupInterface, CodegroupRepository>();
            services.AddScoped<CurriculumInterface, CurriculumRepository>();
            services.AddScoped<ClassReviewInterface, ClassReviewRepository>();
            services.AddScoped<ClassNoticeInterface, ClassNoticeRepository>();
            services.AddScoped<CurriculumInterface, CurriculumRepository>();
            services.AddScoped<ClassReviewInterface, ClassReviewRepository>();
            services.AddScoped<CSVFileInterface, CSVFileRepository>();
            services.AddScoped<IncamAddfareInterface, IncamAddfareRepository>();
            services.AddScoped<ClassQnaInterface, ClassQnaRepository>();
            services.AddScoped<EmailInterface, EmailRepository>();
            services.AddScoped<TeacherInterface, TeacherRepository>();
            services.AddScoped<PdfInterface, PdfRepository>();
            services.AddScoped<IncamContractInterface, IncamContractRepository>();
            services.AddScoped<EmailLogInterface, EmailLogRepository>();
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option => {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
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

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Fallback", action = "Index" }
                );
            });
        }
    }
}