using CreditCard.API.Configurations;
using CreditCard.Application;
using CreditCard.Domain.Messaging;
using CreditCard.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerConfiguration();

var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

builder.Services
    .Configure<MessageBusConnectionConfigModel>(configuration!.GetSection("RabbitMQ"));

builder.Services
    .AddInfrastructureModule()
    .AddApplicationModule();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.Services.UseMessageBusSetup();

// Aplicar migrations ao iniciar a aplicação
app.UseMigration(configuration);

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
