using Catalog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> List();

        Task<bool> Create(Contact contact);

        Task<bool> Update(Contact contact);

        Task<bool> Delete(string id);
    }
}
