using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;


        public ContactController(IContactRepository contactRepository, ILogger<CatalogController> logger)
        {
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Contact>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            var contacts = await _contactRepository.List();
            return Ok(contacts);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateContact([FromBody] Contact contact)
        {
            return Ok(await _contactRepository.Create(contact));
        }

        [HttpPut]
        [ProducesResponseType(typeof(Contact), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateContact([FromBody] Contact value)
        {
            return Ok(await _contactRepository.Update(value));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteContactById(string id)
        {
            return Ok(await _contactRepository.Delete(id));
        }

    }
}
