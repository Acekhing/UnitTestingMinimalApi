namespace UnitTestingMinimalApi.Extensions
{
    public static class WebAppExtensions
    {
        public static void RegisterDevMiddlewares(this WebApplication app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Player API");
            });
            app.MapControllers();
        }
    }
}
