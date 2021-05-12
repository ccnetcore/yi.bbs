using Autofac;
using Autofac.Extras.DynamicProxy;
using CC.Yi.API.Extension;
using CC.Yi.BLL;
using CC.Yi.Common.Castle;
using CC.Yi.Common.Json;
using CC.Yi.Common.Jwt;
using CC.Yi.DAL;
using CC.Yi.IBLL;
using CC.Yi.IDAL;
using CC.Yi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.API
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                //配置基于策略的验证
                options.AddPolicy("myadmin", policy => policy.RequireRole("admin"));

                options.AddPolicy("lv", policy => policy.RequireClaim("lv", "1"));
            });


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,//是否验证Issuer
                           ValidateAudience = true,//是否验证Audience
                           ValidateLifetime = true,//是否验证失效时间
                           ClockSkew = TimeSpan.FromDays(1),
                           ValidateIssuerSigningKey = true,//是否验证SecurityKey
                           ValidAudience = JwtConst.Domain,//Audience
                           ValidIssuer = JwtConst.Domain,//Issuer，这两项和前面签发jwt的设置一致
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConst.SecurityKey))//拿到SecurityKey
                       };
                   });
            Action<MvcOptions> filters = new Action<MvcOptions>(r =>
            {
                //r.Filters.Add(typeof(DbContextFilter));
            });

            services.AddControllers(filters).AddJsonOptions(options=> {

                options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());

            });
            services.AddSwaggerService();
     

            //配置过滤器

            string connection1 = Configuration["ConnectionStringBySQL"];
            string connection2 = Configuration["ConnectionStringByMySQL"];
            string connection3 = Configuration["ConnectionStringBySQLite"];
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(connection3, b => b.MigrationsAssembly("CC.Yi.API"));//设置数据库
            });



            services.AddCors(options => options.AddPolicy("CorsPolicy",//解决跨域问题
            builder =>
            {
                builder.AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
        }

        //初始化使用函数
        private void InitData(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            { 
                var Db = serviceScope.ServiceProvider.GetService<DataContext>();
                var log= serviceScope.ServiceProvider.GetService<Logger<string>>();
                if (Init.InitDb.Init(Db))
                {
                    log.LogInformation("数据库初始化成功！");
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerService();
            }
            app.UseStaticFiles();
            app.UseErrorHandling();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
        
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            InitData(app.ApplicationServices);
        }
    }
}
