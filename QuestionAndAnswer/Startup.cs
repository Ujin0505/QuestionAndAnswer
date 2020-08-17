using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QuestionAndAnswer.Application.Answers.Mappings;
using QuestionAndAnswer.Application.Common.Behaviours;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Questions.Commands;
using QuestionAndAnswer.Application.Questions.Queries;
using QuestionAndAnswer.Application.Questions.Validators;
using QuestionAndAnswer.Infrastracture.Hubs;
using QuestionAndAnswer.Infrastracture.Services;
using QuestionAndAnswer.Persistence;

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
            
            //validation
            services.AddValidatorsFromAssembly(typeof(CreateQuestionCommandValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            
            //signalr
            services.AddSignalR();
            services.AddTransient<IHubService, HubService>();
            
            //memory cache
            services.AddMemoryCache();
            services.AddSingleton<IQuestionMemoryCacheService, QuestionMemoryCacheService>();
            
            // CORS
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:3000")
                    .AllowCredentials();
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
            
            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<QuestionsHub>("/questionshub"); 
            });
        }
    }
}