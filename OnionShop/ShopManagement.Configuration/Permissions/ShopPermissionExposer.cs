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
                        new PermissonDto(ShopPermission.ListProducts , "ListProducts"),
                        new PermissonDto(ShopPermission.SearchProducts , "SearchProducts"),
                        new PermissonDto(ShopPermission.CreateProduct , "CreateProduct"),
                        new PermissonDto(ShopPermission.EditProduct , "EditProduct"),

                    } 
                },
                {
                    "ProductCategory" , new List<PermissonDto>
                    {
                        new PermissonDto(ShopPermission.ListProductCategories,"ListProductCategories"),
                        new PermissonDto(ShopPermission.SearchProductCategories,"SearchProductCategories"),
                        new PermissonDto(ShopPermission.CreateProductCategory,"CreateProductCategory"),
                        new PermissonDto(ShopPermission.EditProductCategory,"EditProductCategory"),

                    }
                }
            };
        }
    }
}
