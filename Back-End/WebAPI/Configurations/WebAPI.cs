using Dietbox.ECommerce.WebAPI.Configurations.Middlewares;

namespace Dietbox.ECommerce.WebAPI.Configurations
{
    public static class WebAPIConfiguration
    {

        public static IServiceCollection ConfigureAPI(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            // CORS:
            services.AddCors(options =>
            {
                options.AddPolicy("DevelopmentCORS", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                options.AddPolicy("ProductionCORS", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            return services;
        }

        public static IApplicationBuilder UseAPIConfig(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment environment)
        {

            if (environment.IsDevelopment())
            {
                app.UseCors("DevelopmentCORS");
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }
            else
            {
                app.UseCors("ProductionCORS");
                app.UseHsts();
            }

            app.UseAppExceptions(); 
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            return app;
        }
    }
}
