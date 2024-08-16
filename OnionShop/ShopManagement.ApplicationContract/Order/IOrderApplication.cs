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
        string PaymentSucceeded(long orderId, long refId);
        double GetAmountBy(long id);
        void Cancel(long id);
        List<OrderItemViewModel> GetItems(long orderId);

        List<OrderviewModel> Search(OrderSearchModel searchModel);

    }
}
