using GV.ONLINE.FOOD.GUI.Business;
using GV.ONLINE.FOOD.GUI.Datarepository;
using GV.ONLINE.FOOD.GUI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GV.ONLINE.FOOD.GUI.Business
{
    public class AuthenticationBusiness : BaseBusinessLogic
    {
            
        public List<string> signUpBusiness(UserModel userObj)
        {
            try
            {
                using (AuthenticationRepository objAuthenticationBusiness = new AuthenticationRepository())
                {

                    return objAuthenticationBusiness.signUpRepository(userObj);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
