using Encrypt.IO.API.Configurations;
using Encrypt.IO.API.Interfaces;
using Encrypt.IO.API.Models.v1;
using Encrypt.IO.API.Services;
using Encrypt.IO.API.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Encrypt.IO.API
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
            LogConfiguration.Configure();

            services.AddOptions();
            services.AddMvc()
                .AddFluentValidation()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IValidator<MessageModel>, MessageValidator>();
            services.AddSingleton<IEncryptationService, EncryptationService>();

            services.ConfigureSwagger();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseHsts();

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "AES Cipher Encryptation API v1.1");
                s.RoutePrefix = string.Empty;
            });

             app.UseMvc();
        }
    }
}
