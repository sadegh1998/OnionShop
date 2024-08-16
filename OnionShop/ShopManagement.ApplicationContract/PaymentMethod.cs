using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.ApplicationContract
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public PaymentMethod() { }

        private PaymentMethod(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public static List<PaymentMethod> GetList()
        {
            return new List<PaymentMethod>{
            new PaymentMethod(1 , "پرداخت اینترنتی","با انتخاب این گزینه به درگاه پرداخت وصل می شوید و هزینه سفارش را آنلاین پرداخت می کنید"),
            new PaymentMethod(2 , "پرداخت نقدی","با انتخاب این گزینه سفارش شما ثبت می شود و پس از تماس کارشناسان و پرداخت شدن هزینه به صورت نقدی ، سفارش شما نهایی می شود."),

            };
        }
        public static string GetBy(long id)
        {
            return GetList().FirstOrDefault(x => x.Id == id)?.Name;
        }

        
    }
}
