using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace CoreWebAPI.Filters
{
    public class AuthenicationFilter : ActionFilterAttribute
    {
      
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            //    var token = actionContext.Request.Headers.Where(x => x.Key == "token").Select(x => x.Value).FirstOrDefault();
            var token = actionContext.HttpContext.Request.Headers.Where(x => x.Key == "token").Select(x => x.Value).FirstOrDefault() ;

            if (token.Count() > 0)
            {
                string tok = token.First().ToString();
                bool flag;
                try
                {
                    flag = ValidateToken(tok);
                    if (flag)
                    {
                        base.OnActionExecuting(actionContext);
                    }
                    else
                    {
                        var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Token is not Authorized" };
                      //  actionContext.Response = responseMessage;
                        base.OnActionExecuted(null);
                    }
                }
                catch (Exception ex)
                {
                   // Logger.getInstance().Error("Authentication", "Error", ex);
                    throw;
                }
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Token is not provided" };
               // actionContext.Response = response;
            }

        }
        public bool ValidateToken(string tokenId)
        {
            //objUser = new DAUser();
            //DataTable table = objUser.isTokenExist(tokenId);
            //if (table.Rows.Count > 0)
            //{
            //    //DateTime exPiry = objUser.GetTokenExpiry(tokenId);
            //    string exPiry = Convert.ToInt32(ConfigurationManager.AppSettings["TokenExpiryExtenstion"].ToString()).ToString();
            //    objUser.UpdateTokenExpiry(tokenId, exPiry.ToString());
            //    return true;
            //}
            return false;
        }
    }
}
