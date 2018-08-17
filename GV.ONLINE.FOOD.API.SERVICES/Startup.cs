using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GV.ONLINE.FOOD.API.SERVICES.Startup))]

namespace GV.ONLINE.FOOD.API.SERVICES
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
