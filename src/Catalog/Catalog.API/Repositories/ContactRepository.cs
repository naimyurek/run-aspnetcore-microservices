using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IContactContext _contactContext;

        public ContactRepository(IContactContext contactContext)
        {
            _contactContext = contactContext ?? throw new ArgumentNullException(nameof(contactContext));
        }
        public async Task<IEnumerable<Contact>> List()
        {
            return await _contactContext
                            .Contact
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<bool> Create(Contact contact)
        {
            var updateResult  = _contactContext.Contact.InsertOneAsync(contact);
            return true;
        }

        public async Task<bool> Update(Contact contact)
        {
            var updateResult = await _contactContext
                                        .Contact
                                        .ReplaceOneAsync(filter: g => g.Id == contact.Id, replacement: contact);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Contact> filter = Builders<Contact>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _contactContext
                                                .Contact
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
