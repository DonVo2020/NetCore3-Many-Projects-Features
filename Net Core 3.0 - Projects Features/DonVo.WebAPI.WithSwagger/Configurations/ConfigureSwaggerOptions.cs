﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;

namespace DonVo.WebAPI.WithSwagger.Configurations
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Version = description.ApiVersion.ToString(),
                    Title = $"DonVo.WebAPI.WithSwagger {description.ApiVersion}",
                    Description = "Swagger DonVo.WebAPI.WithSwagger"
                });
            }

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "DonVo.WebAPI.WithSwagger.xml"));

            var openApiSecurityScheme = new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter into field the word 'Bearer' following by space and JWT",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme }
            };

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, openApiSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement { { openApiSecurityScheme, new List<string>() } });
        }
    }
}
