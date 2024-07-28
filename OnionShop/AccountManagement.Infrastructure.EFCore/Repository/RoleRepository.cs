using _0_Framework.Domain;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly AccountContext _accountContext;

        public RoleRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public EditRole GetDetails(long id)
        {
            return _accountContext.Roles.Select(x=> new EditRole {Id = x.Id , Name = x.Name }).FirstOrDefault(x=>x.Id == id);       
        }

        public List<RoleViewModel> List()
        {
            return _accountContext.Roles.Select(x => new RoleViewModel { Id = x.Id, Name = x.Name }).ToList();
        }
    }
}
