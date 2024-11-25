using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.Account
{
    public interface IAccountApplication 
    {
        OperationResult Create(CreateAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult Login(Login command);
        void Logout();
        EditAccount GetDetails(long id);
        List<AccountViewModel> GetAccounts();

        List<AccountViewModel> Search(AccountSearchModel search);
        OperationResult ChangePassword(ChanagePassword command);
        AccountViewModel GetAccountBy(long id);
        AccountViewModel GetUserEmailBy(string email);
        AccountViewModel GetUserMobileBy(string mobile);
        OperationResult SetToken(string token,long id);
    }
}
