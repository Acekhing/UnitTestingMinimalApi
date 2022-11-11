using UnitTestingMinimalApi.APIs;
using UnitTestingMinimalApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Register Services
builder.Services.Register();

// Create App instance
var app = builder.Build();

// Register APIs
new PlayerAPIs().Register(app);

if (app.Environment.IsDevelopment())
{
    // Register development middlewares
    app.RegisterDevMiddlewares();
}


// Run App
app.Run();