using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SuperShoes.Security
{
    public class BasicAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public string BasicRealm { get; set; }
        protected string Username { get; set; }
        protected string Password { get; set; }

        public BasicAuthorizationAttribute(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var req = actionContext.Request;
            var auth = req.Headers.Authorization?.Parameter;
            if (String.IsNullOrEmpty(auth))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth)).Split(':');
                var user = new { Name = cred[0], Pass = cred[1] };
                if (user.Name != Username || user.Pass != Password)
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            base.OnAuthorization(actionContext);
            
        }
    }
}