using Ensek_Technical_Test.Core.Repository;
using Ensek_Technical_Test.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace Ensek_Techinical_Test.Infrastructure
{
    public static class RegisterDependencies
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service, string defaultConnection)
        {
            service.AddScoped<IDbConnection>(sp =>  new SqlConnection(defaultConnection));
            service.AddScoped<IMeterReadingRepository, MeterReadingRepository>();
            service.AddScoped<IAccountRepository, AccountRepository >();
            return service;
        }
    }
}
