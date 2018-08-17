using GV.ONLINE.FOOD.GUI.Business;
using GV.ONLINE.FOOD.GUI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace GV.ONLINE.FOOD.API.SERVICES.Controllers
{
    public class AuthenticationServiceController : BaseServiceController<AuthenticationServiceController>
    {  
        [HttpPost]
        public List<string> SignUp(UserModel userObj)
        {
            try
            {
                using (AuthenticationBusiness objAuthenticationBusiness = new AuthenticationBusiness())
                {
                   return  objAuthenticationBusiness.signUpBusiness(userObj);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}