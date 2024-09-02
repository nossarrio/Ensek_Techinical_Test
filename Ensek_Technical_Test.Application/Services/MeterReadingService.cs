
using Ensek_Technical_Test.Application.DTO;
using Ensek_Technical_Test.Core.Entity;
using Ensek_Technical_Test.Core.Repository;
using System.Text.RegularExpressions;

namespace Ensek_Technical_Test.Application.Services
{
    public  class MeterReadingService
    {
        private readonly IMeterReadingRepository _meterReadingRepository;
        private readonly IAccountRepository _accountRepository;

        public MeterReadingService(IMeterReadingRepository meterReadingRepository, IAccountRepository accountRepository)
        {
            _meterReadingRepository = meterReadingRepository;
            _accountRepository = accountRepository;
        }

        public async Task<(int successful, int failed)> ProcessMeterReadingsAsync(IEnumerable<MeterReadingDto> meterReadings)
        {
            int successful = 0, failed = 0;
            var newMeterReadings = new List<MeterReading>();
           
            var accountIds = meterReadings.Select(m => m.AccountId).Distinct();       
            var existingMeterReadings = (await _meterReadingRepository.GetMeterReadingsAsync(accountIds)).ToDictionary(m => m.AccountId);
            var existingAccounts = (await _accountRepository.GetAccountsAsync(accountIds)).ToDictionary(m => m.AccountId);

            foreach (var dto in meterReadings)
            {
                if (IsValidMeterReading(dto, existingMeterReadings, existingAccounts))
                {
                    newMeterReadings.Add(new MeterReading
                    {
                        AccountId = dto.AccountId,
                        MeterReadingDateTime = dto.MeterReadingDateTime,
                        MeterReadValue = dto.MeterReadValue
                    });
                    successful++;
                }
                else
                {
                    failed++;
                }
            }

            if (newMeterReadings.Any())
            {
                await _meterReadingRepository.BulkInsertMeterReadingsAsync(newMeterReadings);
            }

            return (successful, failed);
        }


        private bool IsValidMeterReading(MeterReadingDto dto, Dictionary<int, MeterReading> existingReadings, Dictionary<int, Accounts> existingAccounts)
        {
            // Ensure MeterRead value is in the format NNNNN
            if (!Regex.IsMatch(dto.MeterReadValue, @"^\d{5}$"))
            {
                return false;
            }

            // Ensure account exist
            if (!existingAccounts.TryGetValue(dto.AccountId, out var existingAccount))
            {
                return false;
            }

            // Check if the meter reading is older than or same as the existing record
            if (existingReadings.TryGetValue(dto.AccountId, out var existingReading))
            {
                if (dto.MeterReadingDateTime <= existingReading.MeterReadingDateTime)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
