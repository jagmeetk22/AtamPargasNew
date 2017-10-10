using AtamPargas.Core;
using AtamPargas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AtamPargas.Models.Enum;

namespace AtamPargas.Web.Areas.Subjects.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SubjectManager _subjectManager;
        public HomeController()
        {
            this._subjectManager = new SubjectManager();
        }
        public ActionResult Index()
        {
            ViewBag.PanelTitle = "Index";

            var subjects = _subjectManager.GetAllSubject<SubjectDto>();
            return View();
        }
        public ActionResult Subjects()
        {
            ViewBag.PanelTitle = "Subjects";
            return View(_subjectManager.ParentSubjects());
        }
        public ActionResult SubSubjects()
        {
            ViewBag.PanelTitle = "Sub-Subjects";
            return View(_subjectManager.SubSubjects());
        }
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.PanelTitle = "Add Subject";
            return View();
        }
        [HttpPost]
        public ActionResult Add(SubjectDto model)
        {
            ViewBag.PanelTitle = "Add Subject";

            try
            {
                if (ModelState.IsValid)
                {

                    //---Check If Subject Code Exists ---//
                    bool codeExists = _subjectManager.CheckIfCodeExists(model.SubjectCode.ToUpper());

                    if (codeExists)
                    {
                        ModelState.AddModelError("SubjectCode", "Subject code already exists. Try another.");
                        return View(model);
                    }
                    else
                    {
                        if (User.Identity.GetUserId() != null)
                        {
                            model.SubjectCode = model.SubjectCode.ToUpper();
                            model.AddedBy = User.Identity.GetUserId();
                            model.AddedOn = DateTime.Now;
                            model.ModifiedBy = User.Identity.GetUserId();
                            model.ModifiedOn = DateTime.Now;
                            bool added = _subjectManager.Add(model);
                            if (added)
                            {
                                ViewBag.Success = "true";
                                ModelState.Clear();
                                return View();
                            }
                        }
                        else
                        {
                            return View(model);
                        }
                    }
                }
                else
                {
                    return View(model);
                }
                return View();
            }
            catch
            {
                ViewBag.Success = "false";
                return View(model);
            }

        }
        public ActionResult Delete(int id)
        {
            try
            {
                Tuple<bool,bool> deleted = _subjectManager.Delete(id);
                if(deleted.Item1)
                {
                    if(deleted.Item2)
                    {
                        return RedirectToAction("SubSubjects");
                    }
                    else
                    {
                        return RedirectToAction("Subjects");
                    }
                }
                else
                {
                    if (deleted.Item2)
                    {
                        return RedirectToAction("SubSubjects");
                    }
                    else
                    {
                        return RedirectToAction("Subjects");
                    }
                }
            }
            catch
            {
                return null;
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.PanelTitle = "Edit Subject";
            var subject = _subjectManager.GetById(id);
            return View(subject);
        }
        [HttpPost]
        public ActionResult Edit(SubjectDto model)
        {
            try
            {
                model.ModifiedBy = User.Identity.GetUserId();
                bool updated = _subjectManager.UpdateById(model);
                if (updated)
                {
                    ViewBag.Message = "success";
                    return View();
                }
                ViewBag.Message = "failure";
                return View(model);
            }
            catch
            {

                ViewBag.Message = "failure";
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult AssignSubSubjectsToSubject(int id,string subjectName)
        {
            ViewBag.PanelTitle = "Assign Sub-Subjects To Subject";
            var subSubjects = new List<SubjectDto>();
            try
            {
                if (id > 0)
                {
                    ViewBag.SubjectId = id;
                    ViewBag.SubjectName = subjectName;
                    subSubjects = _subjectManager.GetSubSubjectsOfSubjectById(id);
                }
            }
            catch
            {
                var obj = new {Error="Kindly Try Again" };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            return View(subSubjects);
        }
        public ActionResult UpdateSubSubjectOfSubject(int parentSubjectId,int subSubjectId,bool checkBoxChecked)
        {
            try
            {
                bool updated = _subjectManager.UpdateSubSubjectOfSubject(parentSubjectId, subSubjectId, checkBoxChecked);
                if(updated)
                {
                    return Json(new
                    {
                        statusCode = StatusCode.Success,
                        message = CommonMessages.successfullyUpdated
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch 
            {
                return Json(new
                {
                    statusCode = StatusCode.Error,
                    message = CommonMessages.error
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                statusCode = StatusCode.Error,
                message = CommonMessages.error
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubjectCodeListByCode(string code)
        {
            List<string> codeList = _subjectManager.GetSubjectCodeListByCode(code.ToUpper());
            return Json(codeList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CheckIfSubjectHaveSubSubjects(int id)
        {
            try
            {
                bool exists = _subjectManager.CheckIfSubjectHaveSubSubjects(id);
                if(exists)
                {
                    return Json(new
                    {
                        statusCode = TrueFalse.True,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new
                {
                    statusCode = TrueFalse.False,
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                statusCode = TrueFalse.False,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}