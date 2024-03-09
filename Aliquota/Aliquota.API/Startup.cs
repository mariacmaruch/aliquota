using Aliquota.Domain.Interfaces.Repositories;
using Aliquota.Domain.Interfaces.Services;
using Aliquota.Domain.Services.Services;
using Aliquota.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Aliquota.API
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
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ALIQUOTA API"
                });
            });

            var connectionString = Configuration.GetConnectionString("AliquotaCs");
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connectionString,
                            x => x.MigrationsAssembly("Aliquota.Infrastructure")));

            //Usuário
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            //Conta
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<IContaService, ContaService>();

            //Produto 
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ALIQUOTA API");
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            builder.WithOrigins("http://localhost:3000", "http://localhost:57352/")
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }

    }
}
