using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication1.DataBase;
using WebApplication1.Models.ClientData;

namespace WebApplication1.Controllers.Customers
{
    public class CustomersController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            customer.Password = "";
            customer.PasswordConfirmation = "";
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Password,PasswordConfirmation,Email,RegisterOn,EditOn")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    customer.EditOn = DateTime.Now; //Set edit time
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException exception)
            {
                Console.WriteLine(exception.Message);
            }

            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            customer.Password = "";
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "Id,Password")] Customer customer)
        {
            Customer removingCustomer = db.Customers.Find(customer.Id);

            if (removingCustomer == null)
            {
                return HttpNotFound();
            }

            if (removingCustomer.Password == customer.Password)
            {
                db.Customers.Remove(removingCustomer);
                db.SaveChanges();

                return RedirectToAction("Logout", "Login");
            }

            TempData["message"] = "Password not right!";
            return View(removingCustomer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
