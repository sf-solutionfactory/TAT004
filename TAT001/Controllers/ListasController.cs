using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAT001.Models;

namespace TAT001.Controllers
{
    public class ListasController : Controller
    {
        // GET: Listas
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Clientes(string Prefix)
        {
            if (Prefix == null)
                Prefix = "";
            //Note : you can bind same list from database  
            List<City> ObjList = new List<City>()
            {

                new City {Id=1,CityName="Latur" },
                new City {Id=2,CityName="Mumbai" },
                new City {Id=3,CityName="Pune" },
                new City {Id=4,CityName="Delhi" },
                new City {Id=5,CityName="Dehradun" },
                new City {Id=6,CityName="Noida" },
                new City {Id=7,CityName="Nfdgdfgdfgdfgdfgfdgdfgdfew Delhi" },
                new City {Id=7,CityName="Mexico" },
                new City {Id=1,CityName="Latur" },
                new City {Id=2,CityName="Mumbai" },
                new City {Id=3,CityName="Pune" },
                new City {Id=4,CityName="Delhi" },
                new City {Id=5,CityName="Dehradun" },
                new City {Id=6,CityName="Noida" },
                new City {Id=7,CityName="New Delhi" },
                new City {Id=7,CityName="Mexico" },
                new City {Id=1,CityName="Latur" },
                new City {Id=2,CityName="Mumbai" },
                new City {Id=3,CityName="Pune" },
                new City {Id=4,CityName="Delhi" },
                new City {Id=5,CityName="Dehradun" },
                new City {Id=6,CityName="Noida" },
                new City {Id=7,CityName="New Delhi" },
                new City {Id=7,CityName="Mexico" },
                new City {Id=1,CityName="Latur" },
                new City {Id=2,CityName="Mumbai" },
                new City {Id=3,CityName="Pune" },
                new City {Id=4,CityName="Delhi" },
                new City {Id=5,CityName="Dehradun" },
                new City {Id=6,CityName="Noida" },
                new City {Id=7,CityName="New Delhi" },
                new City {Id=7,CityName="Mexico" },
                new City {Id=1,CityName="Latudfgdfgdfgdfgdfgdfgdfgdfgdfgfdgdfgdfr" },
                new City {Id=2,CityName="Mumbai" },
                new City {Id=3,CityName="Pune" },
                new City {Id=4,CityName="Delhi" },
                new City {Id=5,CityName="Dehradun" },
                new City {Id=6,CityName="Noida" },
                new City {Id=7,CityName="New Delhi" },
                new City {Id=7,CityName="Mexico" },
                new City {Id=1,CityName="Latur" },
                new City {Id=2,CityName="Mumbai" },
                new City {Id=3,CityName="Pune" },
                new City {Id=4,CityName="Delhi" },
                new City {Id=5,CityName="Dehradun" },
                new City {Id=6,CityName="Noida" },
                new City {Id=7,CityName="New Delhi" },
                new City {Id=7,CityName="Mexico" },
                new City {Id=1,CityName="Latur" },
                new City {Id=2,CityName="Mumbai" },
                new City {Id=3,CityName="Pune" },
                new City {Id=4,CityName="Delhi" },
                new City {Id=5,CityName="Dehradun" },
                new City {Id=6,CityName="Noida" },
                new City {Id=7,CityName="New Delhi" },
                new City {Id=7,CityName="Mexico" }

        };
            //Searching records from list using LINQ query  
            var CityList = (from N in ObjList
                            where N.CityName.Contains(Prefix)
                            select new { N.CityName });
            JsonResult A = Json(CityList, JsonRequestBehavior.AllowGet);
            return A;
        }
    }
}