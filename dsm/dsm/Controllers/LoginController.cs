using dsm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dsm.Controllers
{
    public class LoginController : Controller
    {

        fmsEntities fe = new fmsEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View("_Login");
        }


        [HttpPost]
        public JsonResult UserLogin(user data)
        {

            string msg = "";

            var login_user = fe.users.Where(x => x.username == data.username && x.password == data.password).FirstOrDefault();
             if (login_user!=null)
            {
                Session["userID"] = login_user.uid;
                Session["userName"] = login_user.username;
                msg = "success";

                return Json(new
                {
                    redirectUrl = Url.Action("Index", "Home"),
                    isRedirect = true,
                    sucs_msg = "Login Success, Please wait.."
                });

            }

             else
            {
                msg = "Invalid Username or Password";
            }

          //  return msg;

            return new JsonResult { Data = msg, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }
}