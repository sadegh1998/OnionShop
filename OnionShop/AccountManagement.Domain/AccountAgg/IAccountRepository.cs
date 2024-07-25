using _0_Framework.Domain;
using AccountManagement.Application.Contract.Account;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<long,Account>
    {
        EditAccount GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel search);
    }
}
