using _0_Framework.Domain;
using _0_Framework.Infrstructure;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
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
            var role =  _accountContext.Roles
                .Select(x=> new EditRole 
                {
                    Id = x.Id , 
                    Name = x.Name , 
                    MappedPermissions = MapPermission(x.Permissions)
                }).AsNoTracking().FirstOrDefault(x=>x.Id == id);

            role.Permissions = role.MappedPermissions.Select(x => x.Code).ToList();
            return role;
        }

        private static List<PermissonDto> MapPermission(List<Permission> permissions)
        {
            return permissions.Select(x => new PermissonDto(x.Code,x.Name)).ToList();
        }

        public List<RoleViewModel> List()
        {
            return _accountContext.Roles.Select(x => new RoleViewModel { Id = x.Id, Name = x.Name }).ToList();
        }
    }
}
