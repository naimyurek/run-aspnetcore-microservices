using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.Interfaces
{
    public interface IContactContext
    {
        IMongoCollection<Contact> Contact { get; }
    }
}
