using MongoDB.Bson;
using MongoDB.Driver;
using PropertyManagamentDatabase.Interface;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagametTypes;

namespace PropertyManagamentDatabase
{
    public class MongoDatabase<T> : IDatabase<T> where T: EntityBase
    {
       public const string CONNECTION_STRING_NAME = "db";
        public const string DATABASE_NAME = "propertymanagament";
        public const string COLLECTION_NAME = "propertymanagament";

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _db;


        public MongoDatabase(string collectionName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase(DATABASE_NAME);
        }

        private IMongoCollection<T> collection
        {
            get
            {
                return _db.GetCollection<T>(COLLECTION_NAME);
            }
            set {
                collection = value;
            }
        }

        public IQueryable<T> Query
        {
            get
            {
                return collection.AsQueryable<T>();
            }
            set
            {
                Query = value;
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

        public async void Create(T item)
        {
           await  collection.InsertOneAsync(item);
        }

        public void DeleteAll()
        {
            _db.DropCollection(typeof(T).Name);
        }

        public bool Update(T item)
        {
            ObjectId id = new ObjectId(typeof(T).GetProperty("Id").GetValue(item, null).ToString());
            var filter_builder = Builders<T>.Filter;
            var filter = filter_builder.Eq("_id", id);

            var result = collection.ReplaceOne<T>(doc => doc.Id == id, item, new UpdateOptions { IsUpsert = true });
            return result.ModifiedCount == 1;

        }

        //public IMongoCollection<Property> Property
        //{
        //    get { return _db.GetCollection<Property>(COLLECTION_NAME); }
        //}
    } 
}
