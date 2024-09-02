using Ensek_Technical_Test.Core.Entity;

namespace Ensek_Technical_Test.Core.Repository
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Accounts>> GetAccountsAsync(IEnumerable<int> accountIds);
    }
}
