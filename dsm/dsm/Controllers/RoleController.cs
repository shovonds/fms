using dsm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dsm.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        fmsEntities fe = new fmsEntities();
        

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult addRole()
        {
            return View();
        }

        [HttpGet]
        public JsonResult allMenuList()
        {
            //List<app_menu> am = new List<app_menu>();
            //am.Add(new app_menu
            //{
            //    menu_id=1,
            //    menu_name="Add user",
            //    method="Home",
            //    action="AddUser",
            //    alias="adduser"
                
            //});

            string msg = "";

            if (Session["userID"] !=null)
            {
                if (ModelState.IsValid)
                {
                    var menulist = fe.app_menu.ToList();
                    return Json(menulist, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                msg = "Unauthorised";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddRole(role rl)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                if (Session["userID"] != null)
                {
                    string r_id = "R" + DateTime.Now.ToString("yyMMddHHmmss");
                    rl.role_id = r_id;
                    fe.roles.Add(rl);
                    fe.SaveChanges();
                    msg = r_id;
                }

                else
                {
                    msg = "Invalid";
                }
                 
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult SaveRoleMenu(role_menu rolemenu)
        {
            string msg = "";
            //rolemenu.menu_id = 1;
            //rolemenu.role_id = "123";
            if (ModelState.IsValid)
            {
                fe.role_menu.Add(rolemenu);
                fe.SaveChanges();
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

    }
}