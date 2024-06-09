using _0_Framework.Application;
using _0_Framework.Domain;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using ShopManagement.Infrastracture.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount> , IColleagueDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;
        public ColleagueDiscountRepository(DiscountContext discountContext, ShopContext shopContext) : base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _discountContext.ColleagueDiscounts.Select(x => new EditColleagueDiscount { 
            Id = x.Id,
            ProductId = x.ProductId ,
            DiscountRate = x.DiscountRate
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _discountContext.ColleagueDiscounts.Select(x => new ColleagueDiscountViewModel {
                Id = x.Id,
            ProductId = x.ProductId ,
            DiscountRate = x.DiscountRate,
            CreationDate = x.CreationDate.ToFarsi() ,
            IsRemove = x.IsRemoved 
            });

            if(searchModel.ProductId > 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            var colleagueDiscounts = query.OrderByDescending(x=>x.Id).ToList();
            colleagueDiscounts.ForEach(x => x.Product = products.FirstOrDefault(p => p.Id == x.ProductId).Name);
            return colleagueDiscounts;
        }
    }
}
