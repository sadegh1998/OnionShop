using _0_Framework.Application;
using _0_Framework.Domain;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastracture.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;
        public CustomerDiscountRepository(DiscountContext discountContext, ShopContext shopContext) : base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _discountContext.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                Id = x.Id,
                DiscoutRate = x.DiscoutRate,
                StartDate = x.StartDate.ToString(),
                EndDate = x.EndDate.ToString(),
                Reason = x.Reason,
                ProductId = x.ProductId,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _discountContext.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscoutRate = x.DiscoutRate,
                Reason = x.Reason,
                StartDate = x.StartDate.ToString(),
                EndDate = x.EndDate.ToString(),
                StartDateGr = x.StartDate,
                EndDateGr = x.EndDate,
            });

            if (search.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == search.ProductId);
            }
            if (string.IsNullOrWhiteSpace(search.StartDate))
            {
                query = query.Where(x => x.StartDateGr > search.StartDate.ToGeorgianDateTime());
            }
            if (string.IsNullOrWhiteSpace(search.EndDate))
            {
                query = query.Where(x => x.EndDateGr > search.EndDate.ToGeorgianDateTime());
            }

            var discounts = query.OrderByDescending(x => x.Id).ToList();

            discounts.ForEach(discount => discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);
            return discounts;
        }
    }
}
