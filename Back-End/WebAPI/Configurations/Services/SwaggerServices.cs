using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Dietbox.ECommerce.WebAPI.Configurations.Services
{
    public static class SwaggerServices
    {

        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddSwaggerGen(config =>
            {
                var assemblyInfo = GetAssemblyInfo();
                bool isDevelopment = (environment.IsDevelopment());
                var enviromentMsg = isDevelopment ? "Ambiente de Desenvolvimento" : "Ambiente de Produção";

                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = assemblyInfo.productAttribute.Product,
                    Description = $"{assemblyInfo.descriptionAttribute.Description}. {enviromentMsg}",
                    Version = assemblyInfo.assemblyName.Version.ToString(),
                    Contact = new OpenApiContact
                    {
                        Name = assemblyInfo.copyrightAttribute.Copyright,
                        Email = "leonardo.valcarenghi@gmail.com.br"
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "RESTRICT USE LICENSE",
                        Url = new Uri(@"https://leonardovalcarenghi.com.br")
                    }
                });

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                config.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { $"{api.GroupName}" };
                    }

                    if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Incapaz de determinar a tag para o endpoint.");
                });

                config.DocInclusionPredicate((name, api) => true);
            });


        }


        private static (AssemblyDescriptionAttribute descriptionAttribute, AssemblyProductAttribute productAttribute, AssemblyCopyrightAttribute copyrightAttribute, AssemblyName assemblyName) GetAssemblyInfo()
        {
            var assembly = typeof(Program).Assembly;
            var assemblyInfo = assembly.GetName();

            var descriptionAttribute = assembly
                 .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
                 .OfType<AssemblyDescriptionAttribute>()
                 .FirstOrDefault();
            var productAttribute = assembly
                 .GetCustomAttributes(typeof(AssemblyProductAttribute), false)
                 .OfType<AssemblyProductAttribute>()
                 .FirstOrDefault();
            var copyrightAttribute = assembly
                 .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)
                 .OfType<AssemblyCopyrightAttribute>()
                 .FirstOrDefault();

            return (descriptionAttribute, productAttribute, copyrightAttribute, assemblyInfo);
        }

    }
}
