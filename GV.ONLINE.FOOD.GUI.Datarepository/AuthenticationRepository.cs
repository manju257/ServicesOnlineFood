using GV.ONLINE.FOOD.GUI.Common;
using GV.ONLINE.FOOD.GUI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GV.ONLINE.FOOD.GUI.Datarepository
{
    public class AuthenticationRepository : BaseDataRepository
    {

        public List<string> signUpRepository(UserModel userObj)
        {

           List<string> obj = new List<string>();
            DataTable  dt;
            try
            {
                using (OnlineFoodDataContext dbcontext = new OnlineFoodDataContext())
                {

                    List<SqlParameter> lstparams = new List<SqlParameter>();
                    lstparams.Add(new SqlParameter("PhoneNumber", userObj.phoneNumber));
                    lstparams.Add(new SqlParameter("Email", userObj.email));
                    dt = dbcontext.ExecuteQueryAndReturnDataReader("Usp_RegisterUser", lstparams, true);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            bool IsActive = dr.ConvertField<bool>("IsAuthenticated");
                            string Email = dr.ConvertField<string>("Email");
                           // if (!IsActive)
                             //   GenerateOTP(Email);

                            obj.Add(Email);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return obj;
           
        }

        public List<string> GenerateOTP(string emailD)
        {


            string strNewPassword = GeneratePassword().ToString();

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("manju22571@gmail.com");
            msg.To.Add(emailD);
            msg.Subject = "Random Password for your Account";
            msg.Body = "Your Random password is:" + strNewPassword;
            msg.IsBodyHtml = true;

            SmtpClient smt = new SmtpClient();
            smt.Host = "smtp.gmail.com";
            System.Net.NetworkCredential ntwd = new NetworkCredential();
            ntwd.UserName = "manju22571@gmail.com"; //Your Email ID  
            ntwd.Password = "Jesus1989"; // Your Password  
            smt.UseDefaultCredentials = true;
            smt.Credentials = ntwd;
            smt.Port = 587;
            smt.EnableSsl = true;
            smt.Send(msg);
            //lblMessage.Text = "Email Sent Successfully";
            //lblMessage.ForeColor = System.Drawing.Color.ForestGreen;
            return null;

        }


        public string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";


            char[] sep = {
            ','
                 };
            string[] arr = allowedChars.Split(sep);


            string IDString = "";
            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }
            return NewPassword;
        }

    }
}
