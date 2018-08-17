using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GV.ONLINE.FOOD.GUI.Datarepository
{
    public class BaseDataRepository :IDisposable
    {

        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            disposed = true;


        }


    }
}
