using GV.ONLINE.FOOD.GUI.Common;
using GV.ONLINE.FOOD.GUI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GV.ONLINE.FOOD.GUI.Datarepository
{
    public class HomeRepository : BaseDataRepository
    {

        public List<CategoryModel> GetCategoryListDataRepository(CategoryModel objCategoryModel)
        {

            List<CategoryModel> lstobj = new List<CategoryModel>();
            DataTable dt;
            try
            {
                using (OnlineFoodDataContext dbcontext = new OnlineFoodDataContext())
                {

                    List<SqlParameter> lstparams = new List<SqlParameter>();
                    lstparams.Add(new SqlParameter("RegionID", "1"));

                    dt = dbcontext.ExecuteQueryAndReturnDataReader("Usp_GetCategoryList", lstparams, true);
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            CategoryModel objcategory = new CategoryModel();
                            objcategory.categoryId = dr.ConvertField<int>("CategoryId");
                            objcategory.categoryName = dr.ConvertField<string>("CategoryName");
                            lstobj.Add(objcategory);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return lstobj;

        }

        public List<SubCategoryModel> GetSUbCategoryListDataRepository(CategoryModel objCategoryModel)
        {

            List<SubCategoryModel> lstobj = new List<SubCategoryModel>();
            DataTable dt;
            try
            {
                using (OnlineFoodDataContext dbcontext = new OnlineFoodDataContext())
                {

                    List<SqlParameter> lstparams = new List<SqlParameter>();
                    lstparams.Add(new SqlParameter("CategoryID", objCategoryModel.categoryId));

                    dt = dbcontext.ExecuteQueryAndReturnDataReader("Usp_GetSubCategoryList", lstparams, true);
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            SubCategoryModel objSubcategory = new SubCategoryModel();
                            objSubcategory.SubCategoryId = dr.ConvertField<int>("SubCategoryId");
                            objSubcategory.SubCategoryName = dr.ConvertField<string>("SubCategoryName");
                            lstobj.Add(objSubcategory);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return lstobj;

        }




    }
}
