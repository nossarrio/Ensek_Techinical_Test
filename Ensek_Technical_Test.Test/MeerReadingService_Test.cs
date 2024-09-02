using Ensek_Technical_Test.Application.DTO;
using Ensek_Technical_Test.Application.Services;
using Ensek_Technical_Test.Core.Entity;
using Ensek_Technical_Test.Core.Repository;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Ensek_Technical_Test.Test
{
    [TestFixture]
    public class MeterReadingServiceTests
    {
        private Mock<IMeterReadingRepository> _meterReadingRepositoryMock;
        private Mock<IAccountRepository> _accountRepositoryMock;
        private MeterReadingService _service;

        [SetUp]
        public void SetUp()
        {
            _meterReadingRepositoryMock = new Mock<IMeterReadingRepository>();
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _service = new MeterReadingService(_meterReadingRepositoryMock.Object, _accountRepositoryMock.Object);
        }

        [Test]
        public async Task ProcessMeterReadingsAsync_ShouldReturnCorrectSuccessAndFailCounts()
        {
            // Arrange
            var existingReadings = new List<MeterReading>
            {
                new MeterReading { AccountId = 1, MeterReadingDateTime = new DateTime(2024, 9, 1), MeterReadValue = "12345" }
            };

            var meterReadings = new List<MeterReadingDto>
            {
                new MeterReadingDto { AccountId = 1, MeterReadingDateTime = new DateTime(2024, 9, 1), MeterReadValue = "12345" }, //reading already exist, should fail
                new MeterReadingDto { AccountId = 2, MeterReadingDateTime = new DateTime(2024, 9, 1), MeterReadValue = "23456" }, // new valid meter reading for esiting account, should succeed
                new MeterReadingDto { AccountId = 1, MeterReadingDateTime = new DateTime(2024, 8, 31), MeterReadValue = "34567" }, // Older reading, should fail
                new MeterReadingDto { AccountId = 3, MeterReadingDateTime = new DateTime(2024, 9, 1), MeterReadValue = "4A678" }, // Invalid reading format, should fail
                new MeterReadingDto { AccountId = 1, MeterReadingDateTime = new DateTime(2024, 9, 2), MeterReadValue = "44600" }, // new valid meter reading for existing account, should succeed
                new MeterReadingDto { AccountId = 4, MeterReadingDateTime = new DateTime(2024, 9, 1), MeterReadValue = "79090" } // account does not exist,should fail
           };

            var existingAccount = new List<Accounts>
            {
                new Accounts { AccountId = 1, FirstName = "firstname1",  LastName = "lastname1" },
                new Accounts { AccountId = 2, FirstName = "firstname2",  LastName = "lastname2" },
                new Accounts { AccountId = 3, FirstName = "firstname3",  LastName = "lastname3" },
            };

            _accountRepositoryMock.Setup(r => r.GetAccountsAsync(It.IsAny<IEnumerable<int>>()))
              .ReturnsAsync(existingAccount);

            _meterReadingRepositoryMock.Setup(r => r.GetMeterReadingsAsync(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(existingReadings);

         
            // Act
            var result = await _service.ProcessMeterReadingsAsync(meterReadings);

            // Assert
            result.successful.Should().Be(2);
            result.failed.Should().Be(4);

            _meterReadingRepositoryMock.Verify(r => r.BulkInsertMeterReadingsAsync(It.Is<IEnumerable<MeterReading>>(m => m.Count() == 2)), Times.Once);
        }

        [Test]
        public async Task ProcessMeterReadingsAsync_ShouldNotInsertWrongMeterReadingFormat()
        {
            // Arrange
            var existingReadings = new List<MeterReading>();

            _meterReadingRepositoryMock.Setup(r => r.GetMeterReadingsAsync(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(existingReadings);

            var meterReadings = new List<MeterReadingDto>
            {
                new MeterReadingDto { AccountId = 1, MeterReadingDateTime = new DateTime(2024, 9, 1), MeterReadValue = "12A45" }, // Invalid format
                new MeterReadingDto { AccountId = 1, MeterReadingDateTime = new DateTime(2024, 9, 1), MeterReadValue = "123" },   // Invalid length
            };

            // Act
            var result = await _service.ProcessMeterReadingsAsync(meterReadings);

            // Assert
            result.successful.Should().Be(0);
            result.failed.Should().Be(2);

            _meterReadingRepositoryMock.Verify(r => r.BulkInsertMeterReadingsAsync(It.IsAny<IEnumerable<MeterReading>>()), Times.Never);
        }

        [Test]
        public async Task ProcessMeterReadingsAsync_ShouldNotInsertInvalidAccounts()
        {
            // Arrange
            var existingAccount = new List<Accounts>
            {
                new Accounts { AccountId = 1, FirstName = "firstname1",  LastName = "lastname1" },
                new Accounts { AccountId = 9, FirstName = "firstname2",  LastName = "lastname2" },
            };

            var meterReadings = new List<MeterReadingDto>
            {
                new MeterReadingDto { AccountId = 1, MeterReadingDateTime = new DateTime(2024, 9, 1), MeterReadValue = "12345" },
                new MeterReadingDto { AccountId = 2, MeterReadingDateTime = new DateTime(2024, 9, 1), MeterReadValue = "54321" },
            };

            _meterReadingRepositoryMock.Setup(r => r.GetMeterReadingsAsync(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new List<MeterReading>());

            _accountRepositoryMock.Setup(r => r.GetAccountsAsync(It.IsAny<IEnumerable<int>>()))
               .ReturnsAsync(existingAccount);
            

            // Act
            var result = await _service.ProcessMeterReadingsAsync(meterReadings);

            // Assert
            result.successful.Should().Be(1);
            result.failed.Should().Be(1);
            _meterReadingRepositoryMock.Verify(r => r.BulkInsertMeterReadingsAsync(It.IsAny<IEnumerable<MeterReading>>()), Times.Once);
        }

        [Test]
        public async Task ProcessMeterReadingsAsync_ShouldSkipOlderReadings()
        {
            // Arrange
            var existingReadings = new List<MeterReading>
            {
                new MeterReading { AccountId = 1, MeterReadingDateTime = new DateTime(2024, 9, 1), MeterReadValue = "12345" }
            };

            var meterReadings = new List<MeterReadingDto>
            {
                new MeterReadingDto { AccountId = 1, MeterReadingDateTime = new DateTime(2024, 8, 31), MeterReadValue = "11111" }, // Older reading
            };

            _meterReadingRepositoryMock.Setup(r => r.GetMeterReadingsAsync(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(existingReadings);
                       

            // Act
            var result = await _service.ProcessMeterReadingsAsync(meterReadings);

            // Assert
            result.successful.Should().Be(0);
            result.failed.Should().Be(1);

            _meterReadingRepositoryMock.Verify(r => r.BulkInsertMeterReadingsAsync(It.IsAny<IEnumerable<MeterReading>>()), Times.Never);
        }
    }
}