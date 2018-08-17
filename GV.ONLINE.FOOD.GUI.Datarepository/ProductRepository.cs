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
    public class ProductRepository : BaseDataRepository
    {
        public List<ProductsModel> GetProductDetailsDataRepository(SubCategoryModel objSubCategoryModel)
        {

            List<ProductsModel> lstobj = new List<ProductsModel>();
            DataTable dt;
            try
            {
                using (OnlineFoodDataContext dbcontext = new OnlineFoodDataContext())
                {

                    List<SqlParameter> lstparams = new List<SqlParameter>();
                    lstparams.Add(new SqlParameter("SubCategoryID", objSubCategoryModel.SubCategoryId));

                    dt = dbcontext.ExecuteQueryAndReturnDataReader("Usp_GetProductDetails", lstparams, true);
                    if (dt.Rows.Count > 0 && dt != null)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            ProductsModel objSubcategory = new ProductsModel();
                            objSubcategory.ProductID = dr.ConvertField<int>("ProductID");
                            objSubcategory.ProductName = dr.ConvertField<string>("ProductName");
                            objSubcategory.SubCategoryId = dr.ConvertField<int>("SubCategoryId");
                            objSubcategory.ProductPrice = dr.ConvertField<int>("ProductPrice");
                            objSubcategory.ProductImage = dr.ConvertField<byte[]>("ProductImage");
                            //objSubcategory.ProductUpdateTime = dr.ConvertField<DateTime?>("ProductUpdateTime");
                            objSubcategory.ProductCartDesc = dr.ConvertField<string>("ProductCartDesc");
                            objSubcategory.ProductShortDesc = dr.ConvertField<string>("ProductLongDesc");
                            //objSubcategory.ProductLive = dr.ConvertField<bool?>("ProductLive");
                            objSubcategory.ProductThumb = dr.ConvertField<string>("ProductThumb");
                            objSubcategory.ProductVendor = dr.ConvertField<string>("ProductVendor");
                            //objSubcategory.ProductInstock = dr.ConvertField<char?>("ProductInstock");
                            lstobj.Add(objSubcategory);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lstobj;

        }
    }

}

