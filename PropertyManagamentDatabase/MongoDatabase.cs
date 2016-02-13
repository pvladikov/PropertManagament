using MongoDB.Bson;
using MongoDB.Driver;
using PropertyManagamentDatabase.Interface;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagamentDatabase
{
    public class MongoDatabase<T> : IDatebase<T> where T : class, new ()
    {
        private IMongoDatabase db;
        private string collectionName;

        private IMongoCollection<T> collection
        {
            get
            {
                return db.GetCollection<T>(collectionName);
            }
            set {
                collection = value;
            }
        }
        public bool Delete(T item)
        {
            ObjectId id = new ObjectId(typeof(T).GetProperty("Id").GetValue(item, null).ToString());
            var filter_builder = Builders<T>.Filter;
            var filter = filter_builder.Eq("_id", id);

            var result = collection.DeleteOne(filter);

            return result.DeletedCount == 1;

        }

        public void Add(T item)
        {
            collection.InsertOne(item);
        }

        public void DeleteAll()
        {
            db.DropCollection(typeof(T).Name);
        }
    }
}
