using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoudInSight.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace LoudInSight.api
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
            // requires using Microsoft.Extensions.Options
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddControllers();
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssue"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
            //Global Authorize filter
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            SwaggerExtension.AddSwagger(services);
            //      //services.AddSwaggerGen();
            //      services.AddSwaggerGen(c =>
            //      {
            //                //c.SwaggerDoc("v1", new Info { Title = "API WSVAP (WebSmartView)", Version = "v1" });
            //                // c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            //                c.SwaggerDoc("v1", new OpenApiInfo { Title = "You api title", Version = "v1" });
            //          c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //          {
            //              Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
            //                                  Enter 'Bearer' [space] and then your token in the text input below.
            //                                  \r\n\r\nExample: 'Bearer 12345abcdef'",
            //              Name = "Authorization",
            //              In = ParameterLocation.Header,
            //              Type = SecuritySchemeType.ApiKey,
            //              Scheme = "Bearer"
            //          });

            //          c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            //{
            //                    {
            //                      new OpenApiSecurityScheme
            //                      {
            //                        Reference = new OpenApiReference
            //                          {
            //                            Type = ReferenceType.SecurityScheme,
            //                            Id = "Bearer"
            //                          },
            //                          Scheme = "oauth2",
            //                          Name = "Bearer",
            //                          In = ParameterLocation.Header,

            //                        },
            //                        new List<string>()
            //                      }
            //          });

            //      });

            DependencyContainer.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");

                c.RoutePrefix = string.Empty;

            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            //app.UseMvcWithDefaultRoute();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
