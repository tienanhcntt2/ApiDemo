using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ApiDemo.config
{
   
    public class Connect
    {
        IMongoDatabase db;
        public Connect(IConfiguration iPconfig)
        {
            var client = new MongoClient(iPconfig.GetConnectionString("FHS"));
            db = client.GetDatabase("FHS");

        }
        public IMongoDatabase getConnect()
        {
            return db;
        }
    }
}
