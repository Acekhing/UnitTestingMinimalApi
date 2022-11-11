namespace UnitTestingMinimalApi.Extensions
{
    public static class AppExtensions
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
        }
    }
}
