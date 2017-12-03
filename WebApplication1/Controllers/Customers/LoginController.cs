using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Facebook;
using Microsoft.Owin.Security;
using WebApplication1.DataBase;
using WebApplication1.Models.ClientData;

namespace WebApplication1.Controllers.Customers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly StoreContext _db = new StoreContext();

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Name,Password")] Customer customer)
        {
            try
            {
                var user = _db.Customers.FirstOrDefault(check => check.Name == customer.Name);

                if (user == null)
                {
                    TempData["message"] = "Username or password is wrong";
                    return Login();
                }

                if (user.Password == customer.Password)
                {
                    ClaimUser(user);
                    return RedirectToAction("Index", "Customers");
                }

                TempData["message"] = "Username or password is wrong";
                return View();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return View(customer);
        }

        // GET: Customers/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Customers/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,Name,Password,PasswordConfirmation,Email,RegisterOn,EditOn")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _db.Customers.FirstOrDefault(check => check.Name == customer.Name);

                    if (user != null)
                    {
                        TempData["message"] = "The username is already exist!";
                        return Register();
                    }

                    var fbId = TempData["fbId"];
                    customer.FacebookId = (string)fbId;
                    _db.Customers.Add(customer);
                    _db.SaveChanges();
                    ClaimUser(customer);
                    return RedirectToAction("Index", "Customers");
                }
            }
            catch (DbEntityValidationException exception)
            {
                Console.WriteLine(exception.Message);
            }

            return View(customer);
        }

        public ActionResult ExternalLogin()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "2323575231033585",
                client_secret = "39d2db54637abb2f03c6e4b229c7d1a1",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email" // Add other permissions as needed
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallBack(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "2323575231033585",
                client_secret = "39d2db54637abb2f03c6e4b229c7d1a1",
                redirect_uri = RedirectUri.AbsoluteUri,
                code
            });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic userInfo = fb.Get("me?fields=id,name,email");
            string fbId = userInfo.id;
            var user = _db.Customers.FirstOrDefault(check => check.FacebookId == fbId);

            if (user != null)
            {
                ClaimUser(user);
                return RedirectToAction("Index", "Customers");
            }

            TempData["fbId"] = fbId;
            return RedirectToAction("Register");
        }

        // GET: Logout
        public ActionResult Logout()
        {
            var authentication = Request.GetOwinContext().Authentication;
            authentication.SignOut();
            return View();
        }

        private void ClaimUser(Customer user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var identity = new ClaimsIdentity(claims, "WebApplication1Cookie");
            Request.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
