using AutoMapper;
using DonVo.DI;
using DonVo.SpecialConfigurations;
using DonVo.ViewModels.Extensions;
using DonVo.WebAPI.WithSwagger.Configurations;
using DonVo.WebAPI.WithSwagger.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO.Compression;
using MainAutoMapperProfile = DonVo.SpecialConfigurations.AutoMapperProfile;
using ServicesAutoMapperProfile = DonVo.SpecialConfigurations.AutoMapperProfile;

namespace DonVo.WebAPI.WithSwagger
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", reloadOnChange: true, optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(Configuration["RestConnection:ElasticUrl"]))
                {
                    AutoRegisterTemplate = true
                })
                .WriteTo.Console(LogEventLevel.Information)
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var authOptions = Configuration.GetSection("AuthOptions").Get<AuthModel>();
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });

            services
               .AddDependencyInjectionServiceModule(
                   Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>(),
                   Configuration.GetSection("HashIdConfiguration").Get<HashIdConfiguration>(),
                   Configuration.GetSection("RestConnection").Get<RestConnection>());

            services.AddResponseCompression()
                    .Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; })
                    .AddResponseCompression(options => { options.EnableForHttps = true; });

            services.AddApiVersioning(options => options.ReportApiVersions = true)
                    .AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; options.SubstituteApiVersionInUrl = true; })
                    .AddSwaggerGen();

            services.AddControllers()
                    .AddDonVoValidationModule();

            DependencyInjectionSystem(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseResponseCompression();
            //app.UseCustomMiddlewares();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                c.DocExpansion(DocExpansion.None);
                c.EnableValidator();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void DependencyInjectionSystem(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AuthModel>(Configuration.GetSection("AuthOptions"));
            services.AddAutoMapper(typeof(MainAutoMapperProfile), typeof(ServicesAutoMapperProfile));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IJwtBearerConfiguration, JwtBearerConfiguration>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }
    }
}
