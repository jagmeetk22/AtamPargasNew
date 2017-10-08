using AtamPargas.Core;
using AtamPargas.Models;
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
    }
}