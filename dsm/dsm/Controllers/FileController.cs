using dsm.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dsm.Controllers
{
    public class FileController : Controller
    {
        // GET: File

        fmsEntities fe = new fmsEntities();


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual string UploadFiles(object obj)
        {
            string user_id = Session["userID"].ToString();
            string file_type = "unknown";

            var length = Request.ContentLength;
            var bytes = new byte[length];
            Request.InputStream.Read(bytes, 0, length);

            var fileName = Request.Headers["X-File-Name"];
            var fileSize = Request.Headers["X-File-Size"];
           // var fileType = Request.Headers["X-File-Type"];
            var fileDir = Request.Headers["X-File-dir"];

            int index = fileName.IndexOf(".");
            if (index>0)
            {
                file_type = fileName.Substring(fileName.LastIndexOf(".")+1);
            }

            int size = Convert.ToInt32(fileSize);

            var saveToFileLoc = Server.MapPath("../Storage/") + user_id + "/" + fileDir + "\\"+ fileName;

            // save the file.
            var fileStream = new FileStream(saveToFileLoc, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(bytes, 0, length);
            fileStream.Close();


            SaveDocs(fileName, size, file_type, fileDir);
            return string.Format("{0} bytes uploaded", bytes.Length);
        }

        public void SaveDocs(string fileName, int fileSize, string fileType, string Dir_id)
        {
            document doc = new document();
            string msg;

            doc.creation_date = DateTime.Now;
            doc.name = fileName;
            doc.size = fileSize;
            doc.dir_id = Dir_id;
            doc.type = fileType;

            try
            {
                if (ModelState.IsValid)
                {
                    fe.documents.Add(doc);
                    fe.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                msg = ex.Message;
            }

        }


        [HttpGet]
        public JsonResult fileList(string dir_id)
        {
            string msg = "";

            if (Session["userID"]!=null)
            {
                var file_list = fe.documents.Where(x => x.dir_id == dir_id).ToList();
                return Json(file_list, JsonRequestBehavior.AllowGet);
            }

            return Json(msg= "Unauthorized", JsonRequestBehavior.AllowGet);


        }


    }
}