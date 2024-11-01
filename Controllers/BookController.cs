using _6351071034_LTWEB_K63.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _6351071034_LTWEB_K63.Controllers
{
    public class BookController : Controller
    {
        private QLBansachEntities db = new QLBansachEntities();
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

    }
}