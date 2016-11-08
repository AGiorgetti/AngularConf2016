using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
	/// <summary>
	/// this must be called the same as the folder that will contain the AngularJs Javascript application
	/// for the relative path to work correctly (in angular routes and templates).
	/// </summary>
    public class AngularJsController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}