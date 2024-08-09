using _0_Framework.Application;
using _0_Framework.Infrstructure;
using _01_ShopQuery.Contracts.Order;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.ApplicationContract.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_ShopQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;
        public CartCalculatorService(DiscountContext discountContext, IAuthHelper authHelper)
        {
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        public Cart CamputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var currentAcountRole = _authHelper.CurrentAccountRole();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsRemoved)
                .Select(x => new { x.ProductId, x.DiscountRate });

            var customerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscoutRate, x.EndDate });

            foreach (var cartItem in cartItems)
            {
                if (currentAcountRole == Roles.ColleagueUser)
                {
                    var colleagueDiscount = colleagueDiscounts.Where(x => x.ProductId == cartItem.Id).FirstOrDefault();
                    if (colleagueDiscount != null)
                    {
                        cartItem.DiscountRate = colleagueDiscount.DiscountRate;
                    }
                }
                else
                {
                    var customerDiscount = customerDiscounts.Where(x => x.ProductId == cartItem.Id).FirstOrDefault();
                    if (customerDiscount != null)
                    {
                        cartItem.DiscountRate = customerDiscount.DiscoutRate;
                    }
                }
                cartItem.DiscountItemAmount = (cartItem.TotalItemPrice * cartItem.DiscountRate) / 100;
                cartItem.PayItemAmount = cartItem.TotalItemPrice - cartItem.DiscountItemAmount;


                cart.Add(cartItem);
            }
            return cart;


        }
    }
}
