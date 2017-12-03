using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Owin;
using WebApplication1.DataBase;

namespace WebApplication1
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "WebApplication1Cookie",
                LoginPath = new PathString("/Login/Login"),
                ExpireTimeSpan = TimeSpan.FromMinutes(5)
            });

            //app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            //{
            //    AppId = "1771130356522631",
            //    AppSecret = "1f94444f287b59036067247e8dd5f818",
            //    Scope = { "email" },
            //    UserInformationEndpoint = "https://graph.facebook.com/v2.11/me?fields=id,name,email"
            //});
        }
    }
}