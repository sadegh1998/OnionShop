﻿using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.ApplicationContract.ProductCategory
{
    public interface IProductCategoryApplication
    {
        OperationResult Create(CreateProductCategory command);
        OperationResult Edit(EditProductCategory command);
        EditProductCategory Get(long Id);
        List<ProductCategoryViewModel> GetProductCategories();

        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
