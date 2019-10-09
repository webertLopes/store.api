using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Store.Application.Interfaces;
using Store.Application.Services;
using Store.Domain.Entities;
using Store.Domain.Exceptions;
using Store.Domain.Interfaces;
using Store.Infra.Repository;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;

namespace Store.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddFluentValidation();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddSingleton<IValidator<Product>, ProductCoreException>();
            services.AddSingleton<IValidator<Customer>, CustomerCoreException>();
            services.AddSingleton<IValidator<SalesMan>, SalesManCoreException>();


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList();

                    var result = new
                    {
                        Code = "00009",
                        Message = "Validation Errors",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
            });

            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISalesManRepository, SalesManRepository>();
            services.AddScoped<ISalesManService, SalesManService>();

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<FileUploadOperation>();

                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "StoreAPI",
                        Version = "v1",
                        Description = "StoreAPI",
                        Contact = new Contact
                        {
                            Name = "Webert Lopes Cançado",
                            Url = "https://github.com/webertLopes"
                        }
                    });

                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"Store.Api.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }                      

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "StoreAPI");
            });
        }
    }
}
