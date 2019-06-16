using MyTaskApplication.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace MyTaskApplication.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        // Login form validation
        public ActionResult Login(LoginModel user)
        {
            if (ModelState.IsValid)
            
                if (user.IsValid(user.UserName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    return RedirectToAction("Index", "Document");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }

            
            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //Register form validation and insert user into users database table 
        public ActionResult Register(RegisterModel userreg)
        {
            if (ModelState.IsValid)
            {
                // Using Nhibernate to insert user into database
                using(ISession session = NhibernateSession.OpenSession())
                {
                    //In HQL, only the INSERT INTO … SELECT … is supported; there is no INSERT INTO … VALUES. HQL only support insert from another table. "Oh my god :-)"
                    IQuery query = session.CreateSQLQuery("exec sp_add_user @UserName=:User, @Password=:Password, @Email=:Email");
                    query.SetParameter("User",userreg.UserName);
                    query.SetParameter("Password", userreg.Password);
                    query.SetParameter("Email", userreg.Email);
                    query.ExecuteUpdate();
                }

            }
            else
            {
                ModelState.AddModelError("", "Register data is incorrect!");
                return View(userreg);
            }
            return RedirectToAction("RegSuccess", "User");
        }

        //redirect to RegistrationSuccess page
        public ActionResult RegSuccess()
        {
            return View();
        }

    }

}

