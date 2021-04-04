using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Settings;
using MongoDB.Driver;
using System;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        //private readonly IGenericRepository  _genericRepository;
        public CatalogContext(IGenericRepository genericRepository)
        {
            //_genericRepository = _genericRepository ?? throw new ArgumentNullException(nameof(_genericRepository));
            Products = genericRepository.MongoDatabase.GetCollection<Product>("Products");
           // CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
