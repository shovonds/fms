using dsm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dsm.Controllers
{
    public class HomeController : Controller
    {

        fmsEntities fe = new fmsEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult AddUser()
        //{
        //    string msg = "success";

        //    return new JsonResult { Data = msg, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}


        public ActionResult AddUser()
        {
            return View();
        }


        [HttpPost]
        public JsonResult addNewUser(user newUser)
        {
             
            string msg = "";
            if (ModelState.IsValid)
            {
                var userList = fe.users.Where(x => x.username == newUser.username).ToList();
                if (userList.Count<1)
                {
                    newUser.created_date = DateTime.Now;
                    newUser.uid = newUser.username;
                    newUser.password = newUser.username + "@123";

                    fe.users.Add(newUser);
                    fe.SaveChanges();
                    msg = "New user added, User:"+ newUser.username+", Password:"+ newUser.password;
                }
                else
                {

                    msg = "User already exist, please try with different user name";
                }

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

    }
}