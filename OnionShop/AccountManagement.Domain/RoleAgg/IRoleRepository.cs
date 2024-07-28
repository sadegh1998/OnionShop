using _0_Framework.Domain;
using AccountManagement.Application.Contract.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IRepository<long , Role>
    {
        EditRole GetDetails(long id);
        List<RoleViewModel> List();
    }
}
