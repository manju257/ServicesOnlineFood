using GV.ONLINE.FOOD.GUI.Datarepository;
using GV.ONLINE.FOOD.GUI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GV.ONLINE.FOOD.GUI.Business
{
    public class HomeBusiness : BaseBusinessLogic
    {

        public List<CategoryModel> GetCategoryListsBusiness(CategoryModel objCategoryModel)
        {
            try
            {
                using (HomeRepository objHomeBusiness = new HomeRepository())
                {

                    return objHomeBusiness.GetCategoryListDataRepository(objCategoryModel);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<SubCategoryModel> GetSubCategoryListsBusiness(CategoryModel objCategoryModel)
        {
            try
            {
                using (HomeRepository objAuthenticationBusiness = new HomeRepository())
                {

                    return objAuthenticationBusiness.GetSUbCategoryListDataRepository(objCategoryModel);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
