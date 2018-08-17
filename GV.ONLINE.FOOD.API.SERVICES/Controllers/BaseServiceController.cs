using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GV.ONLINE.FOOD.API.SERVICES.Controllers
{
    public class BaseServiceController<T> : ApiController
    {

        public static readonly ILog log = log4net.LogManager.GetLogger(typeof(T));


    }
}