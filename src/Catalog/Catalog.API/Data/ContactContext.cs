using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Settings;
using MongoDB.Driver;
using System;

namespace Catalog.API.Data
{
    public class ContactContext : IContactContext
    {
        public ContactContext(IGenericRepository genericRepository)
        {
            Contact = genericRepository.MongoDatabase.GetCollection<Contact>("Contact");
        }

        public IMongoCollection<Contact> Contact { get; }
    }
}
