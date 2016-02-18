using Newtonsoft.Json;
using PropertyManagamentDatabase;
using PropertyManagamentDatabase.Interface;
using PropertyManagametTypes;
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
    public class ServiceController : Controller
    {
        IDatabase<Property> propertyRepo;
        IDatabase<Owner> ownerRepo;

        public ServiceController() : this(new MongoDatabase<Property>(), new MongoDatabase<Owner>())
        {

        }

        public ServiceController(MongoDatabase<Property> categoriesRepo, MongoDatabase<Owner> ownerRepo)
        {
            this.propertyRepo = categoriesRepo;
            this.ownerRepo = ownerRepo;
        }


        [HttpGet]
        public JsonResult PropertyManagament()
        {
            //IEnumerable<Property> properties = PropertyManagamentRepository.PropertyManagamentRepository.ToList();
            var properties = propertyRepo.Query;

            return Json(properties, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ContentResult Property()
        {
            Property property = new Property();
            propertyRepo.Create(property);
            var json = JsonConvert.SerializeObject(property, new MyStringEnumConverter());

            return new ContentResult { Content = json, ContentType = "application/json" };
        }

        [HttpPut]
        public JsonResult EditProperty(Property property)
        {
            //PropertyManagamentRepository.PropertyManagamentRepository.Update(property);
            propertyRepo.Update(property);

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