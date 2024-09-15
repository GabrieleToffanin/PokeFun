using PokeFun.Application.PokemonUseCases.GetPokemonInformation;
using PokeFun.Application.Services;
using PokeFun.Application.Services.Abstractions;
using PokeFun.Infrastructure.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRouting(options => options.LowercaseUrls = true); // Thank you C# for enforcing UpperCase by default :D
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //Configure MediatR
        builder.Services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(GetPokemonRequestHandler).Assembly));

        //Add ApplicationServices
        builder.Services.AddScoped<IPokemonService, PokemonService>();

        //Add External services adapters
        builder.Services.RegisterExternalServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}