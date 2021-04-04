using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Settings;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class GenericRepository : IGenericRepository
    {
        public GenericRepository(ICatalogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            MongoDatabase = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoDatabase MongoDatabase { get; }
    }
}
