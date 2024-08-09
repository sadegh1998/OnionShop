using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.ApplicationContract.Order
{
    public interface ICartService
    {
        Cart Get();
        void Set(Cart cart);
    }
}
