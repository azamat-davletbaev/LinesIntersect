using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinesDLL;
using Newtonsoft.Json;

namespace LinesIntersect.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }        
        [HttpPost]
        public string GetLines()
        {
            var lines = LoadData.GetLines();            

            return JsonConvert.SerializeObject(lines);
        }

        [HttpPost]
        public string GetPolygon()
        {
            var rez = LoadData.GetPolygon();
            return JsonConvert.SerializeObject(rez);
        }

       

    }
}