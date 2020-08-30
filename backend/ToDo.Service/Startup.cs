using System;
using AutoMapper;
using ToDo.Database;
using ToDo.Database.Repository;
using ToDo.Service.Controllers.Dtos;
using ToDo.Service.Controllers.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ToDo.Service.Internals;
using System.Reflection;
using System.IO;
using Serilog;
using ToDo.Api.Internals;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Google;

namespace ToDo.Service
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
            services.AddDbContext<ToDoContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
            services.AddControllers(opt => opt.Filters.Add<ActionLoggingFilter>())
                .AddNewtonsoftJson();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddGoogle(options =>
                {
                    options.ClientId = "43180207284-c7cljolm8vsa20anmot6len0nm1av8qi.apps.googleusercontent.com";
                    options.ClientSecret = "aDXJYaPgz8VxY44_bFGeZLYW";
                    //options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    //options.CallbackPath = new PathString("/signin-google");
                    // options.ReturnUrlParameter = new PathString("/token");
                    // options.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                    // options.ClaimActions.Clear();
                    // options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    // options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    // options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                    // options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                    // options.ClaimActions.MapJsonKey("urn:google:profile", "link");
                    // options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                    //options.EventsType = typeof(IOAuthExternalProviderEvents);
                });

            services.AddAuthorization();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowCredentials();
                builder.SetIsOriginAllowed(IsOriginAllowed)
                    .WithOrigins(
                        "http://localhost:3000"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            services.AddTransient<AbstractValidator<CreateToDoDto>, ToDoValidator>();
            services.AddScoped<ToDoContext>();
            services.AddScoped<IToDoRepository, ToDoRepository>();
            services.AddScoped<ICsvBuilder, CsvBuilder>();
            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ToDo API", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

                c.AddSecurityDefinition("Oauth2", new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.OAuth2,
                    Description = "ToDo Auth",
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://accounts.google.com/signin/oauth/identifier"),
                            Scopes = new Dictionary<string, string>
                            {
                                {
                                    "openid",
                                    "ToDoApplication"
                                }
                            }
                        }
                    }
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Oauth2"
                            }
                        },
                        new List<string>
                        {
                            "openid"
                        }
                    }
                });
            });
        }

        private static bool IsOriginAllowed(string host)
        {
            return true;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.OAuth2RedirectUrl("https://localhost:5001/signin-google");
                opt.OAuthClientId("43180207284-c7cljolm8vsa20anmot6len0nm1av8qi.apps.googleusercontent.com");
                opt.OAuthClientSecret("aDXJYaPgz8VxY44_bFGeZLYW");
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API");
                opt.RoutePrefix = "ToDo/v1/documentation";
            });
            app.UseRouting();
            app.UseCors("MyPolicy" );
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}