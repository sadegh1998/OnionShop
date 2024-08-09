using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Infrstructure
{
    public static class Roles
    {
        public const string Administrator = "1";
        public const string SiteUser = "2";
        public const string InventoryUser = "4";
        public const string ColleagueUser = "5";


        public static string GetRoleBy(long id)
        {
            switch (id)
            {
                case 1:
                    return "مدیرسیستم";
                case 3:
                    return "کاربر سیستم";
                    case 4:
                    return "کاربر انبارداری";
                default:
                    return "";
            }
        }
    }
   
}
