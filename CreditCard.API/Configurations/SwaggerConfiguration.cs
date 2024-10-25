using Microsoft.OpenApi.Models;

namespace CreditCard.API.Configurations;

public static class SwaggerConfiguration
{
    public static void AddSwaggerConfiguration(this IServiceCollection service)
    {
        service.AddApiVersioning();

        service.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Credit Proposal",
                Description = "API Documentation",
                Version = "1.0",
                Contact = new OpenApiContact { Name = "Gabrielle", Url = new Uri("https://gabrielleraujo.github.io/") }
            });

            options.EnableAnnotations();
        });
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Credit Proposal v1.0");
        }); 
    }
}