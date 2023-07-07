using Dietbox.ECommerce.WebAPI.Configurations;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment environment = builder.Environment;
IServiceCollection services = builder.Services;
IConfiguration configurations = builder.Configuration;
services.ConfigureAPI();
services.RegisterServices(configurations, environment);

WebApplication app = builder.Build();
app.UseAPIConfig(configurations, app.Environment);
app.Run();
