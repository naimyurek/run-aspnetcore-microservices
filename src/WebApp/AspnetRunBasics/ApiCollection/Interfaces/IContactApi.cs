using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.ApiCollection.Interfaces
{
    public interface IContactApi
    {
        Task<IEnumerable<ContactViewModel>> GetContact();
        Task<IActionResult> CreateContact(ContactViewModel contactModel);
        Task<IActionResult> UpdateContact(ContactViewModel contactModel);
        Task<IActionResult> DeleteContact(string id);
    }
}
