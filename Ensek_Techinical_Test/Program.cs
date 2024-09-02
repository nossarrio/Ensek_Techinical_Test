using Ensek_Techinical_Test;
using Ensek_Techinical_Test.Application;
using Ensek_Techinical_Test.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();
app.UseStaticFiles();
app.RegisterErrorHandler();
app.RegisterEndpoints();
app.Run();
 