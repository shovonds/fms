using dsm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dsm.Controllers
{
    public class DirectoryController : Controller
    {
        // GET: Directory


        fmsEntities fe = new fmsEntities();




        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyStorage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddNewDir(directory d)
        {
            string user_id = Session["userID"].ToString();
            string msg = "";

            string dir_nid = DateTime.Now.ToString("yyyyMMddHHmmss");
            d.created_date = DateTime.Now;
            d.dir_id = dir_nid;
           // d.parent_id = 

            creatDir(dir_nid);

            fe.directories.Add(d);
            fe.SaveChanges();
            AddUserDir(user_id, dir_nid);

            return new JsonResult { Data = msg, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public void creatDir(string dir_nid)
        {
            string user_id = Session["userID"].ToString();

            string targetPath = Server.MapPath("../Storage/") + user_id + "/" + dir_nid;  //with complete path
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);

            }
          
        }

        public void AddUserDir(string user_id, string dir_id)
        {
            user_directory ud = new user_directory();
        
            ud.user_id = user_id;
            ud.dir_id = dir_id;

            fe.user_directory.Add(ud);
            fe.SaveChanges();

        }

        [HttpGet]
        public JsonResult UserDirList()
        {
            string user_id = Session["userID"].ToString();

            var dir_list = (from Directory in fe.directories
                         join User_directory in fe.user_directory on Directory.dir_id equals User_directory.dir_id
                         where
                           User_directory.user_id == user_id //Convert.ToString(2)
                          
                            select new
                         {
                             Directory.dir_id,
                             Directory.name,
                             Directory.parent_id,
                             User_directory.user_id
                         });

            return Json(dir_list,JsonRequestBehavior.AllowGet);
        }

        public void GetDirTreeList (List<directory> list, directory current, ref List<directory> returnList)
        {
            var chlds = list.Where(a => a.parent_id == current.dir_id).ToList();
            current.child = new List<directory>();
            current.child.AddRange(chlds);

            foreach (var i in chlds)
            {
                GetDirTreeList(list, i, ref returnList);
            }

        }

        public List<directory> BuildDirTree(List<directory> list)
        {
            List<directory> returnList = new List<directory>();
            var top = list.Where(a => a.parent_id == list.OrderBy(b => b.parent_id).FirstOrDefault().parent_id);
            returnList.AddRange(top);

            foreach (var i in top)
            {
                GetDirTreeList(list, i, ref returnList);
            }

            return returnList;
        }


        [HttpGet]
        public JsonResult GetDirStructure()
        {
            string user_id = Session["userID"].ToString();
           // List<directory> list = new List<directory>();

            user_directory ud = new user_directory();
            directory dir = new directory();

            string query = "Select * from directory where dir_id in(Select dir_id from user_directory where user_id = @p0)";
            var list = fe.Database.SqlQuery<directory>(query,user_id).ToList();

            List <directory> treeDir = new List<directory>();
            if (list.Count > 0)
            {
                treeDir = BuildDirTree(list);
                
            }

            return new JsonResult { Data = new { treeDirectory = treeDir }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}