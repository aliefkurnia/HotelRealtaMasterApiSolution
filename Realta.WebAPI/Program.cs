using Realta.Services.Abstraction;
using Realta.Services;
using Realta.WebAPI.Extensions;
using Microsoft.Extensions.FileProviders;

internal class Program
{

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ILoggerManager logger = new LoggerManager();

        // Add services to the container.
        builder.Services.ConfigureCors();
        builder.Services.ConfigureIISIntegration();
        builder.Services.ConfigureDbContext(builder.Configuration);
        builder.Services.ConfigureRepositoryManager();
        builder.Services.ConfigureUtitlityService();
        builder.Services.ConfigureLoggerService();


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.ConfigureExceptionHandler(logger);
        app.UseHttpsRedirection();

        //add custom
        app.UseStaticFiles();
        // set folder resources to static file
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
            RequestPath = new PathString("/Resources")
        });
        app.UseCors("CorsPolicy");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}