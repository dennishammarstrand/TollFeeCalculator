using Microsoft.AspNetCore.Builder;

namespace Api.Configuration.Swagger
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseSwaggerDocuments(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Toll Fee");
                c.RoutePrefix = string.Empty;
            });
            return app;
        }
    }
}
