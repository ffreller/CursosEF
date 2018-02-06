using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursosEF.Contextos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace CursosEF
{
    public class Startup
    {
        IConfiguration Configuration{get;}
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c => {c.SwaggerDoc("v1", new Info
            {
                Version = "v1",
                Title = "Api Forum",
                Description = "Doc",
                TermsOfService = "None",
                Contact = new Contact
                {
                    Name = "Fernando",
                    Email = "email",
                    Url = "www"
                }
            });
        });

            services.AddDbContext<CursosContexto>(options=>options.UseSqlServer(Configuration.GetConnectionString("BancoCursosEF")));
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI( c => {c.SwaggerEndpoint("/swagger/v1/swagger.json","Api Cursos Online");});
            app.UseMvc();
           
        }
    }
}