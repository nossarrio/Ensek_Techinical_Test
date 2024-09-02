using Ensek_Technical_Test.Core.Entity;

namespace Ensek_Technical_Test.Core.Repository
{
    public interface IMeterReadingRepository
    {
        Task<IEnumerable<MeterReading>> GetMeterReadingsAsync(IEnumerable<int> accountIds);
        Task<int> BulkInsertMeterReadingsAsync(IEnumerable<MeterReading> meterReadings);
    }
}
