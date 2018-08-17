using GV.ONLINE.FOOD.GUI.Datarepository;
using GV.ONLINE.FOOD.GUI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GV.ONLINE.FOOD.GUI.Business
{
   public class ProductBusiness : BaseBusinessLogic
    {



        public List<ProductsModel> GetProductDetailsBusiness(SubCategoryModel objSubCategoryModel)
        {
            try
            {
                using (ProductRepository objAuthenticationBusiness = new ProductRepository())
                {

                    return objAuthenticationBusiness.GetProductDetailsDataRepository(objSubCategoryModel);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
