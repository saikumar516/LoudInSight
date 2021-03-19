using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoudInSight.api
{
    internal static class SwaggerExtension
    {
        internal static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new Info { Title = "API WSVAP (WebSmartView)", Version = "v1" });
                // c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "You api title", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                                        Enter 'Bearer' [space] and then your token in the text input below.
                                        \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                          {
                            new OpenApiSecurityScheme
                            {
                              Reference = new OpenApiReference
                                {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                              },
                              new List<string>()
                            }
                });
                //c.OperationFilter<AddRequiredHeaderParameter>();
            });
        }
    }
	class PerformParametersFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
            if (operation.Parameters == null)
                return;
            var parameters = context.ApiDescription.ActionDescriptor.Parameters;
			foreach (var parameter in parameters)
			{
				foreach (var property in parameter.ParameterType.GetProperties())
				{
					var param = operation.Parameters.FirstOrDefault(o => o.Name.ToLowerInvariant().Contains(property.Name.ToLowerInvariant()));
                    if (param == null) continue;
                    //var name=
				}
			}
		}
	}
	class AddRequiredHeaderParameter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
            operation.Parameters.Add(new OpenApiParameter
            {
                Name="Authorization",
                In= ParameterLocation.Header,
                Description="Jwt Token",
                Required=false,
               
            });
		}
	}
}
