using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AtamPargas.Core;
using System.IO;

namespace AtamPargas.Web.Controllers
{
    public class HomeController : Controller
    {

        //private readonly SubjectManager _subjectManager;

        //public HomeController() : this(new SubjectManager()) {
        //}

        //public HomeController(SubjectManager subjectManager)
        //{
        //    this._subjectManager = subjectManager;
        //}

        //public HomeController()
        //{
        //    this._subjectManager = new SubjectManager();
        //}


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult UploadFile()
        {
            return View();
        }
        public ActionResult UploadFilePost(HttpPostedFileBase files)
        {

            var fileContent = Request.Files[0];

            if (fileContent.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(fileContent.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                fileContent.SaveAs(_path);
            }
            return Json("",JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult UploadFileJquery(HttpPostedFileBase files)
        {

            var fileContent = Request.Files[0];

            if (fileContent.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(fileContent.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                fileContent.SaveAs(_path);
            }
            return Json("success", JsonRequestBehavior.AllowGet);

        }
    }
}