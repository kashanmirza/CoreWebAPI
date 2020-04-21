using CoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
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
        CoreDBContext db = new CoreDBContext();
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
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
                        var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Token is not Authorized OR Expired" };
                        actionContext.Result = new BadRequestObjectResult(responseMessage);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Token is not provided" };
                actionContext.Result = new BadRequestObjectResult(response);
                return;
            }

        }
        public bool ValidateToken(string tokenId)
        {

            if (!String.IsNullOrEmpty(tokenId)) {
                var user = db.SecUsers.Where(x => x.Token.Contains(tokenId)).FirstOrDefault();
                if(user != null)
                {

                    if (user.TokenExpireOn >= DateTime.Now)
                    {
                        user.TokenExpireOn = DateTime.Now.AddMinutes(5);
                        db.SecUsers.Update(user);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
