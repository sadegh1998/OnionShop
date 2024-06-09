using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operation = new OperationResult();
            if(_colleagueDiscountRepository.Exisit(x=>x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.IsRemoved == false))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }

            var colleagueDiscount = new ColleagueDiscount(command.ProductId,command.DiscountRate);
            _colleagueDiscountRepository.Create(colleagueDiscount);
            _colleagueDiscountRepository.SaveChanges();
            operation.Success();
            return operation;
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(command.Id);
            if(colleagueDiscount == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            if (_colleagueDiscountRepository.Exisit(x => x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.IsRemoved == false && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.Duplicate);
            }

            colleagueDiscount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChanges();
            operation.Success();
            return operation;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(id);
            if (colleagueDiscount == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }
          
            colleagueDiscount.Remove();
            _colleagueDiscountRepository.SaveChanges();
            operation.Success();
            return operation;
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var colleagueDiscount = _colleagueDiscountRepository.Get(id);
            if (colleagueDiscount == null)
            {
                return operation.Failed(ApplicationMessages.NotFound);
            }

            colleagueDiscount.Restore();
            _colleagueDiscountRepository.SaveChanges();
            operation.Success();
            return operation;
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}
