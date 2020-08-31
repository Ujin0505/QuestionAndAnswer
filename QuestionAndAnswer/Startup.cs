using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QuestionAndAnswer.Application.Answers.Mappings;
using QuestionAndAnswer.Application.Common.Behaviours;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Questions.Commands;
using QuestionAndAnswer.Application.Questions.Queries;
using QuestionAndAnswer.Application.Questions.Validators;
using QuestionAndAnswer.Authorization;
using QuestionAndAnswer.Authorization.Handlers;
using QuestionAndAnswer.Infrastracture.Hubs;
using QuestionAndAnswer.Infrastracture.Services;
using QuestionAndAnswer.Persistence;
using QuestionAndAnswer.Services;

namespace QuestionAndAnswer
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
            services.AddHttpClient();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            
            //db
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(contextOptions =>
            {
                contextOptions.UseNpgsql(Configuration.GetConnectionString("DbConnection"), npgSqlOptions =>
                {
                    npgSqlOptions.MigrationsAssembly("QuestionAndAnswer.Persistence");
                });
            });

            //mediatr
            services.AddMediatR(typeof(GetQuestionQueryHandler).Assembly);
            services.AddValidatorsFromAssembly(typeof(CreateQuestionCommandValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            
            
            //signalr
            services.AddSignalR();
            services.AddTransient<IHubService, HubService>();
            
            //memory cache
            services.AddMemoryCache();
            services.AddSingleton<IQuestionMemoryCacheService, QuestionMemoryCacheService>();
            
            //auth0
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["Auth0:Authority"];
                options.Audience = Configuration["Auth0:Audience"];
            });
            
            //authorization
            services.AddAuthorization(options => options.AddPolicy("Author", policy => policy.Requirements.Add(new AuthorRequirement())));
            services.AddScoped<IAuthorizationHandler, AuthorHandler>(); 
            
            //swagger
            services.AddSwaggerGen(/*setup =>
            {
                setup.SwaggerDoc("v1", new Info { Title = "QAndA API", Version = "v1" });
                
                setup.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });*/

                /*setup.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[0]}
                });
                
            }*/);
                
            // CORS
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins(Configuration["Frontend"]);
            }));

            //automapper
            //services.AddAutoMapper(typeof(CreateAnswerCommandToDomainModel).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
                app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QAndA V1");
            });
            
            
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<QuestionsHub>("/questionshub"); 
            });
        }
    }
}