using GV.ONLINE.FOOD.GUI.Business;
using GV.ONLINE.FOOD.GUI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GV.ONLINE.FOOD.API.SERVICES.Controllers
{
    public class ProductServiceController : BaseServiceController<ProductServiceController>
    {
        [HttpPost]
        public List<ProductsModel> GetProductDetails(SubCategoryModel objSubCategoryModel)
        {
            try
            {
                using (ProductBusiness objHomeBusiness = new ProductBusiness())
                {
                    return objHomeBusiness.GetProductDetailsBusiness(objSubCategoryModel);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}