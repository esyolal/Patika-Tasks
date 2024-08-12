using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation;
using BookStore.Validator;
using BookStore.Services;
using Microsoft.OpenApi.Models;
using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;
namespace BookStore;
public class Startup
{


    public IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddSwaggerGen(c =>
       {
           c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore.Api", Version = "v1" });
       });
        services.AddDbContext<BookStoreDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")));

        services.AddValidatorsFromAssemblyContaining<CreateAuthorDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateAuthorDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateBookDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateBookDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateGenreDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateGenreDtoValidator>();

        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IGenreService, GenreService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
