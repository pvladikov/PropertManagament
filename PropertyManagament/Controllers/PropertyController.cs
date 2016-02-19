using Newtonsoft.Json;
using PropertyManagamentDatabase;
using PropertyManagamentDatabase.Interface;
using PropertyManagametTypes;
using PropertyManagametTypes.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PropertyManagament.Controllers
{
    public class PropertyController : Controller
    {
        IDatabase<Property> propertyRepository;
        IDatabase<Owner> ownerRepository;

        public PropertyController() : this(new MongoDatabase<Property>(), new MongoDatabase<Owner>())
        {

        }

        public PropertyController(MongoDatabase<Property> categoriesRepo, MongoDatabase<Owner> ownerRepo)
        {
            this.propertyRepository = categoriesRepo;
            this.ownerRepository = ownerRepo;
        }


        [HttpGet]
        public ActionResult Read()
        {
            //IEnumerable<Property> properties = PropertyManagamentRepository.PropertyManagamentRepository.ToList();
            var properties = propertyRepository.Query;

            var json = JsonConvert.SerializeObject(properties, new MyStringEnumConverter());
            return new ContentResult { Content = json, ContentType = "application/json" };

            // return Json(properties, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create()
        {
            Property property = new Property();
            propertyRepository.Create(property);
            var json = JsonConvert.SerializeObject(property, new MyStringEnumConverter());

            return new ContentResult { Content = json, ContentType = "application/json" };

            //return Json(property);
        }

        [HttpPost]
        public JsonResult Delete(Property property) {
            var res = propertyRepository.Delete(property);
            return Json(res);
        }

        [HttpPut]
        public JsonResult EditProperty(Property property)
        {
            //PropertyManagamentRepository.PropertyManagamentRepository.Update(property);
            propertyRepository.Update(property);

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