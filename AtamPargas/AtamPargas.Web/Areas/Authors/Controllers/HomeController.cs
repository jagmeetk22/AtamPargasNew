using AtamPargas.Core;
using AtamPargas.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtamPargas.Web.Areas.Authors.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IGenericRepository<AuthorDto> _authorManager;
        public HomeController()
        {
            this._authorManager = new AuthorManager();
        }
        public ActionResult AllAuthors()
        {
            return View(_authorManager.SelectAll().ToList());
        }
        public ActionResult Add(int id = 0)
        {
            ViewBag.PanelTitle = "Add Author";

            if (id > 0)
            {
                AuthorDto author = _authorManager.SelectByID(id);
                if (author != null)
                {
                    return View(author);
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult Add(AuthorDto model)
        {
            ViewBag.PanelTitle = "Add Author";
            try
            {
                if (model.AuthorId > 0)
                {
                    AuthorDto author = _authorManager.SelectByID(model.AuthorId);
                    if (author != null)
                    {
                        bool codeExists = _authorManager.CheckIfCodeExists(model.AuthorCode, model.AuthorId);
                        if (!codeExists)
                        {
                            author.AuthorCode = model.AuthorCode;
                            author.AuthorName = model.AuthorName;
                            author.AuthorNamePunjabi = model.AuthorNamePunjabi;
                            author.AuthorContactNumber = model.AuthorContactNumber;
                            author.AuthorAddress = model.AuthorAddress;
                            author.ModifiedBy = User.Identity.GetUserId();
                            author.ModifiedOn = DateTime.Now;
                            _authorManager.Update(author);
                            ViewBag.Status = CommonMessageCode.Success;
                            ViewBag.Message = CommonMessages.successfullyUpdated;
                            return View(author);
                        }
                        else
                        {
                            ModelState.AddModelError("AuthorCode", "Code already exists. Try another.");
                            return View(model);
                        }
                    }
                    ViewBag.Status = CommonMessageCode.Fail;
                    ViewBag.Message = CommonMessages.error;
                    return View(model);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        //---Check If Subject Code Exists ---//
                        bool codeExists = _authorManager.CheckIfCodeExists(model.AuthorCode.ToUpper(),0);

                        if (codeExists)
                        {
                            ModelState.AddModelError("AuthorCode", "Code already exists. Try another.");
                            return View(model);
                        }
                        else
                        {
                            if (User.Identity.GetUserId() != null)
                            {
                                model.AuthorCode = model.AuthorCode.ToUpper();
                                model.AddedBy = User.Identity.GetUserId();
                                model.AddedOn = DateTime.Now;
                                model.ModifiedBy = User.Identity.GetUserId();
                                model.ModifiedOn = DateTime.Now;
                                _authorManager.Insert(model);
                                ViewBag.Status = CommonMessageCode.Success;
                                ViewBag.Message = CommonMessages.successfullyAdded;
                                ModelState.Clear();
                                return View();
                            }
                            else
                            {
                                return View(model);
                            }
                        }
                    }
                    return View(model);
                }
            }
            catch(Exception ex)
            {
                ViewBag.Status = CommonMessageCode.Fail;
                ViewBag.Message = CommonMessages.error;
                return View(model);
            }

        }
        public ActionResult GetCodeListByCode(string code)
        {
            List<string> codeList = _authorManager.GetCodeListByCode(code.ToUpper());
            return Json(codeList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                _authorManager.Delete(id);
                return RedirectToAction("AllAuthors");
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}