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
using Microsoft.AspNetCore.Http;
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
                //���û��ڲ��Ե���֤
                options.AddPolicy("������", policy => policy.RequireClaim("action", "������"));
                options.AddPolicy("�������", policy => policy.RequireClaim("action", "�������"));
                options.AddPolicy("�ȼ�����", policy => policy.RequireClaim("action", "�ȼ�����"));
                options.AddPolicy("���ù���", policy => policy.RequireClaim("action", "���ù���"));
                options.AddPolicy("�������", policy => policy.RequireClaim("action", "�������"));
                options.AddPolicy("�汾����", policy => policy.RequireClaim("action", "�汾����"));

                options.AddPolicy("�û�����", policy => policy.RequireClaim("action", "�û�����"));
                options.AddPolicy("��ɫ����", policy => policy.RequireClaim("action", "��ɫ����"));
                options.AddPolicy("Ȩ�޹���", policy => policy.RequireClaim("action", "Ȩ�޹���"));




                options.AddPolicy("��ǩ����", policy => policy.RequireClaim("action", "��ǩ����"));
                options.AddPolicy("�ղع���", policy => policy.RequireClaim("action", "�ղع���"));
                options.AddPolicy("��������", policy => policy.RequireClaim("action", "��������"));
                options.AddPolicy("��������", policy => policy.RequireClaim("action", "��������"));
                options.AddPolicy("�޸���Ϣ", policy => policy.RequireClaim("action", "�޸���Ϣ"));
                options.AddPolicy("��ʽ����", policy => policy.RequireClaim("action", "��ʽ����"));
                options.AddPolicy("���޹���", policy => policy.RequireClaim("action", "���޹���"));

            });


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,//�Ƿ���֤Issuer
                           ValidateAudience = true,//�Ƿ���֤Audience
                           ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                           ClockSkew = TimeSpan.FromDays(1),


                           ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                           ValidAudience = JwtConst.Domain,//Audience
                           ValidIssuer = JwtConst.Domain,//Issuer���������ǰ��ǩ��jwt������һ��
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConst.SecurityKey))//�õ�SecurityKey
                       };
                   });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Action<MvcOptions> filters = new Action<MvcOptions>(r =>
            {
                //r.Filters.Add(typeof(DbContextFilter));
            });

            services.AddControllers(filters).AddJsonOptions(options=> {

                options.JsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());

            });
            services.AddSwaggerService();
     

            //���ù�����

            string connection1 = Configuration["ConnectionStringBySQL"];
            string connection2 = Configuration["ConnectionStringByMySQL"];
            string connection3 = Configuration["ConnectionStringBySQLite"];
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(connection3, b => b.MigrationsAssembly("CC.Yi.API"));//�������ݿ�
            });



            services.AddCors(options => options.AddPolicy("CorsPolicy",//�����������
            builder =>
            {
                builder.AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
        }

        //��ʼ��ʹ�ú���
        private void InitData(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            { 
                var Db = serviceScope.ServiceProvider.GetService<DataContext>();

                Init.InitDb.Init(Db);

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
