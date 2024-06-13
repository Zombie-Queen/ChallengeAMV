using AMV.TourReservation.Application.Jwt;
using AMV.TourReservation.Application.MappingProfiles;
using AMV.TourReservation.Application.Services;
using AMV.TourReservation.Infrastructure;
using AMV.TourReservation.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace AMV.TourReservation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDatabaseContext(Configuration);
            services.AddRepositories();

            services.AddAutoMapper(typeof(Startup), typeof(MappingProfile));
            // Registro de servicios de aplicación
            services.AddScoped<ReservationManagerService>();
            services.AddScoped<AuthService>();
            services.AddScoped<JwtGenerator>();

            // Configuración de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AMV TourReservation API",
                    Description = "ASP.NET Core Web API for AMV TourReservation",
                });

                // Ruta al archivo XML de documentación
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var secret = Configuration["Jwt:Secret"];
            var issuer = Configuration["Jwt:Issuer"];
            var audience = Configuration["Jwt:Audience"];
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true,
                    ValidateIssuer = true
                };
            });

            services.AddAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Habilita el middleware para servir el documento Swagger generado como JSON
            app.UseSwagger();

            // Habilita middleware para servir Swagger-ui (HTML, JS, CSS, etc.),
            // especificando el endpoint del documento Swagger JSON.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AMV TourReservation API V1");
                // Para servir Swagger UI en la raíz de la aplicación
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthentication();    
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
