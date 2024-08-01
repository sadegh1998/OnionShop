using _0_Framework.Infrstructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissonDto>> Expose()
        {
            return new Dictionary<string, List<PermissonDto>> 
            {

                {
                    "Product" , new List<PermissonDto>
                    {
                        new PermissonDto(10 , "ListProducts"),
                        new PermissonDto(11 , "SearchProducts"),
                        new PermissonDto(12 , "CreateProduct"),
                        new PermissonDto(13 , "EditProduct"),

                    } 
                },
                {
                    "ProductCategory" , new List<PermissonDto>
                    {
                        new PermissonDto(11,"ListProductCategories"),
                        new PermissonDto(11,"SearchProductCategories"),
                        new PermissonDto(11,"CreateProductCategory"),
                        new PermissonDto(11,"EditProductCategory"),

                    }
                }
            };
        }
    }
}
