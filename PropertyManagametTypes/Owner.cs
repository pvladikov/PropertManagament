using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagametTypes
{
    [BsonIgnoreExtraElements]
    public class Owner
    {
        public ObjectId id { get; private set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string picture_url { get; set; }
    }
}
