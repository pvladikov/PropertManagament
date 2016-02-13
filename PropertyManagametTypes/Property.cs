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
    public class Property
    {
        public ObjectId Id { get; private set; }
        public double area { get; set; }


        public Property()
        {
            area = 100;
        }
    }
}
