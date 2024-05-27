using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDiscount.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult CreateDiscount(DefineCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscountRepository.Exisit(x => x.ProductId == command.ProductId && x.DiscoutRate == command.DiscoutRate))
                return operation.Failed(ApplicationMessages.Duplicate);

            var discount = new DiscountManagement.Domain.CustomerDiscountAgg.CustomerDiscount(command.ProductId,command.DiscoutRate,command.StartDate.ToGeorgianDateTime(),command.EndDate.ToGeorgianDateTime(),command.Reason);
            _customerDiscountRepository.Create(discount);
            _customerDiscountRepository.SaveChanges();
            operation.Success();
            return operation;
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operation = new OperationResult();
            if (_customerDiscountRepository.Exisit(x => x.ProductId == command.ProductId && x.DiscoutRate == command.DiscoutRate && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.Duplicate);

            var discount = _customerDiscountRepository.Get(command.Id);
            if (discount == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }
            discount.Edit(command.ProductId, command.DiscoutRate, command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), command.Reason);
            _customerDiscountRepository.SaveChanges();
            operation.Success();
            return operation;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel search)
        {
            return _customerDiscountRepository.Search(search);
        }
    }
}
