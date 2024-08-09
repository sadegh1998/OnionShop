using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.ApplicationContract.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);
        void PaymentSucceeded(long orderId, long refId);
    }
}
