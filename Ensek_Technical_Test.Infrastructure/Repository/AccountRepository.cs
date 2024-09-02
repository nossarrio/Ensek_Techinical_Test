using Dapper;
using Ensek_Technical_Test.Core.Entity;
using Ensek_Technical_Test.Core.Repository;
using System.Data;


namespace Ensek_Technical_Test.Infrastructure.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly IDbConnection _dbConnection;

        public AccountRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Accounts>> GetAccountsAsync(IEnumerable<int> accountIds)
        {
            var query = "SELECT AccountId, FirstName, LastName FROM Accounts WHERE AccountId IN @AccountIds";
            return await _dbConnection.QueryAsync<Accounts>(query, new { AccountIds = accountIds });
        }
    }
}
