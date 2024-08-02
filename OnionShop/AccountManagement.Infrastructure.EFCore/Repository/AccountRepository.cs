using _0_Framework.Application;
using _0_Framework.Infrstructure;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;

        public AccountRepository(AccountContext context) : base(context) 
        {
           _context = context;
        }

        public Account GetBy(string username)
        {
            return _context.Accounts.FirstOrDefault(x => x.Username == username);
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount {
            Id = x.Id,
            FullName = x.FullName,  
            Mobile = x.Mobile,
            RoleId = x.RoleId,
            Username = x.Username
            })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel search)
        {
            var query = _context.Accounts.Include(x=>x.Role).Select(x => new AccountViewModel { 
            Id = x.Id,
            Username= x.Username,
            FullName = x.FullName ,
            Mobile = x.Mobile,
            ProfilePicture = x.ProfilePicture,
            RoleId = x.RoleId,
            Role = x.Role.Name,
            CreationDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(search.Mobile))
            {
                query = query.Where(x => x.Mobile.Contains(search.Mobile));
            }
            if (!string.IsNullOrWhiteSpace(search.FullName))
            {
                query = query.Where(x => x.FullName.Contains(search.FullName));
            }
            if (!string.IsNullOrWhiteSpace(search.Username))
            {
                query = query.Where(x => x.Username.Contains(search.Username));
            }
            if(search.RoleId > 0)
            {
                query = query.Where(x => x.RoleId == search.RoleId);
            }

            var result = query.OrderByDescending(x => x.Id).ToList();
            return result;
        }
    }
}
