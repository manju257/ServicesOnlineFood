using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GV.ONLINE.FOOD.GUI.Entities
{
   public  class ResultModel<T> where T : class
    {
        T _Data = default(T);

        public T Data
        {
            get { return _Data; }
            set { _Data = value; }
        }

        public int TotalCount { get; set; }

    }
}
