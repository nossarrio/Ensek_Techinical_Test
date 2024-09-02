using Dapper;
using Ensek_Technical_Test.Core.Entity;
using Ensek_Technical_Test.Core.Repository;
using System.Data;


namespace Ensek_Technical_Test.Infrastructure.Repository
{
    public class MeterReadingRepository: IMeterReadingRepository
    {
        private readonly IDbConnection _dbConnection;

        public MeterReadingRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<MeterReading>> GetMeterReadingsAsync(IEnumerable<int> accountIds)
        {
            var query = "SELECT AccountId, MeterReadingDateTime, MeterReadValue FROM MeterReading WHERE AccountId IN @AccountIds";
            return await _dbConnection.QueryAsync<MeterReading>(query, new { AccountIds = accountIds });
        }

        public async Task<int> BulkInsertMeterReadingsAsync(IEnumerable<MeterReading> meterReadings)
        {
            var query = @"INSERT INTO MeterReading (AccountId, MeterReadingDateTime, MeterReadValue) VALUES (@AccountId, @MeterReadingDateTime, @MeterReadValue)";
            return await _dbConnection.ExecuteAsync(query, meterReadings);
        }
    }
}
