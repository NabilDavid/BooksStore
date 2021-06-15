using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Models;
using BooksStore.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BooksStore
{
    public class Startup
    {
        private readonly IConfiguration iconfiguration;

        public Startup(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }

      
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //singleton if you want use memory
            //services.AddSingleton<IBookRepository<Auther>, AutherRepository>();
            //services.AddSingleton<IBookRepository<Book>, BookRepository>();

            // if you want use framework
            services.AddScoped<IBookRepository<Auther>, AuthorRepositoryDb>();
            services.AddScoped<IBookRepository<Book>, BookRepositoryDb>();
            services.AddDbContext<BookStoreDbContext>(Options =>
            {
                Options.UseSqlServer(iconfiguration.GetConnectionString("sqlcon"));

            });
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(route =>
            {
                route.MapRoute("defualt", "{controller=Book}/{action=index}/{id?}");

            });
          app.UseStaticFiles();
        }
    }
}
