﻿using _0_Framework.Application;
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
        EditAccount GetDetails(long id);
        List<AccountViewModel> Search(AccountSearchModel search);
        OperationResult ChangePassword(ChanagePassword command);
    }
}