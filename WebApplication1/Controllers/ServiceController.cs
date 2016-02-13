using PropertyManagametTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropertyManagament.Controllers
{
    public class ServiceController : Controller
    {

        [HttpGet]
        public JsonResult PropertyManagament()
        {
            IEnumerable<Property> properties = PropertyManagamentRepository.PropertyManagamentRepository.ToList();

            return Json(properties, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Property()
        {
            Property property = new Property() { area = 100 };

            return Json(property);
        }
    }
}