using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GV.ONLINE.FOOD.GUI.Entities
{
   public  class ProductsModel
    {

        public int ProductID { get; set; }
        public string    ProductName { get; set; }
        public int?   SubCategoryId { get; set; }
        public int   ProductPrice { get; set; }
        public byte[]  ProductImage { get; set; }
        public DateTime?   ProductUpdateTime { get; set; }
        public string   ProductCartDesc { get; set; }
        public string   ProductShortDesc { get; set; }
        public string   ProductLongDesc { get; set; }
        public bool?   ProductLive { get; set; }
        public string   ProductThumb { get; set; }
        public string   ProductVendor { get; set; }
        public char?   ProductInstock { get; set; }
    }
}
