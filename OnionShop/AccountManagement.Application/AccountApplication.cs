﻿using _0_Framework.Application;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.AccountAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;   
        private readonly IFileUploader _fileUploader;

        public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher, IFileUploader fileUploader)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
            _fileUploader = fileUploader;
        }

        public OperationResult ChangePassword(ChanagePassword command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.Get(command.Id);
            if(account == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            if(command.Password != command.RePassword)
            {
                return operation.Failed(ApplicationMessages.PasswordNotMatch);
            }
            var password = _passwordHasher.Hash(command.Password);
            account.ChanagePassword(password);
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Create(CreateAccount command)
        {
            var operation = new OperationResult();
            if(_accountRepository.Exisit(x=>x.Username == command.Username || x.Mobile == command.Mobile))
            {
                return operation.Failed(ApplicationMessages.UserIsRegisterd);
            }
            var password = _passwordHasher.Hash(command.Password);
            var path = $"ProfilePictures";
            var profilePicture = _fileUploader.Upload(command.ProfilePicture, path);
            var account = new Account(command.FullName,command.Username,password,command.Mobile,1,profilePicture);
            _accountRepository.Create(account);
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();
            if (_accountRepository.Exisit(x => x.Username == command.Username || x.Mobile == command.Mobile && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.UserIsRegisterd);
            }
            var account = _accountRepository.Get(command.Id);

            if (account == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }
            var path = $"ProfilePictures";
            var profilePicture = _fileUploader.Upload(command.ProfilePicture, path);
            account.Edit(command.FullName, command.Username, command.Mobile, 1, profilePicture);
            _accountRepository.SaveChanges();
            return operation.Success();
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }

        public List<AccountViewModel> Search(AccountSearchModel search)
        {
            return _accountRepository.Search(search);
        }
    }
}