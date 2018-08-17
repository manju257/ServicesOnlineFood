using GV.ONLINE.FOOD.GUI.Business;
using GV.ONLINE.FOOD.GUI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GV.ONLINE.FOOD.API.SERVICES.Controllers
{
    public class HomeServiceController  : BaseServiceController<HomeServiceController>
    {
        
        [HttpPost]
        public List<CategoryModel> GetCategoryList(CategoryModel objCategoryModel)
        {
            try
            {
                using (HomeBusiness objHomeBusiness = new HomeBusiness())
                {
                    return objHomeBusiness.GetCategoryListsBusiness(objCategoryModel);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public List<SubCategoryModel> GetSubCategoryList(CategoryModel objCategoryModel)
        {
            try
            {
                using (HomeBusiness objHomeBusiness = new HomeBusiness())
                {
                    return objHomeBusiness.GetSubCategoryListsBusiness(objCategoryModel);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}
