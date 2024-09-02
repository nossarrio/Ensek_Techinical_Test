using Ensek_Technical_Test.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ensek_Techinical_Test.Application
{
    public static class RegisterDependencies
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            service.AddScoped<MeterReadingService>();
            return service;
        }
    }
}
