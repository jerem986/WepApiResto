using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RestoAPP.API.Services;
using RestoAPP.API.Services.Security;
using RestoAPP.API.Utils;
using RestoAPP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ToolBox.Security.Configuration;
using ToolBox.Security.DependencyInjection.Extensions;
using ToolBox.Security.Middlewares;

namespace RestoAPP.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestoAPP.API", Version = "v1" });
                OpenApiSecurityScheme securitySchema = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                c.AddSecurityRequirement(securityRequirement);
            });

            services.AddCors(options => options.AddPolicy("default", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            }));

            #region JWT
            services.AddJwt(Configuration.GetSection("Jwt").Get<JwtConfiguration>());
            #endregion


            #region Dal Service
            services.AddDbContext<RestoDbContext>();
            services.AddScoped<RepasService>();
            services.AddScoped<ClientService>();
            services.AddScoped<ReservationService>();
            services.AddScoped<UserService>();
            services.AddScoped<HashService>();
            services.AddScoped<UserRepository>();
            services.AddScoped<CategoryService>();
            #endregion

            #region Service Mail
            services.AddScoped<MailService>();
            services.AddSingleton(Configuration.GetSection("SMTP").Get<MailConfig>());
            services.AddScoped<SmtpClient>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestoAPP.API v1"));
            }

            app.UseCors("default");

            app.UseHttpsRedirection();

            app.UseMiddleware<JwtHandlerMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
