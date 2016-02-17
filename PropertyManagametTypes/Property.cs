using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;

namespace PropertyManagametTypes
{
    [BsonIgnoreExtraElements]
    public class Property : EntityBase
    {
        //public ObjectId _id { get; set; }
        public string upi { get; set; }
        public List<Owner> owners { get; set; }
        public decimal area { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MannerOfPermanentUsage manner_of_permanent_usage { get; set; }

        public string picture_url { get; set; }
        public Mortgage mortgage { get; set; }

        public Property()
        {
           // area = 100;
        }
    }
   
   [DataContract]
    public enum MannerOfPermanentUsage
    {
        //[EnumMember(Value = "Emp")]
        //[DescriptionAttribute("Test")]
        //[EnumMember(Value ="Test")]
       // [Display(Name = "It is complicated")]
        [DataMember]
        Residentional,
        //[EnumMember]
        Industrial = 20,
       // [EnumMember]
        Agricultural = 30
    }

    public abstract class EntityBase
    {
        public ObjectId Id { get; set; }
    }
}
