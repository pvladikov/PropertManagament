using Newtonsoft.Json;
using PropertyManagametTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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
        public ActionResult Property()
        {
           Property property = new Property();

            var json = JsonConvert.SerializeObject(property, new MyStringEnumConverter());

           return  new ContentResult { Content = json, ContentType = "application/json" };

            // return Json(property);
        }

        [HttpPut]
        public JsonResult EditProperty(Property property)
        {
            PropertyManagamentRepository.PropertyManagamentRepository.Update(property);
                
            return Json(property);
        }
    }

    public class MyStringEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is MannerOfPermanentUsage)
            {
                writer.WriteValue(Enum.GetName(typeof(MannerOfPermanentUsage), (MannerOfPermanentUsage)value));// or something else
                return;
            }

            base.WriteJson(writer, value, serializer);
        }
    }
}