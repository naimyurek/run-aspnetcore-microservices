using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.Interfaces
{
    public interface IGenericRepository
    {
       IMongoDatabase MongoDatabase { get; }
    }
}
