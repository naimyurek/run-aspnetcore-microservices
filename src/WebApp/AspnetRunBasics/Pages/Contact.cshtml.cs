using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AspnetRunBasics
{
    public class ContactModel : PageModel
    {


        private readonly IContactApi _contactApi;

        public ContactModel(IContactApi contactApi)
        {
            _contactApi = contactApi ?? throw new ArgumentNullException(nameof(contactApi));

        }

        public IActionResult OnGet()
        {
            return Page();
        }

#pragma warning disable MVC1002 // Route attributes cannot be applied to page handler methods.
        [HttpPost]
#pragma warning restore MVC1002 // Route attributes cannot be applied to page handler methods.
#pragma warning disable MVC1001 // Filters cannot be applied to page handler methods.
        [ValidateAntiForgeryToken]
#pragma warning restore MVC1001 // Filters cannot be applied to page handler methods.
        public IActionResult OnPostContactForm(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var r = _contactApi.CreateContact(model);
            TempData["ContactInfo"] = "Merhaba " + model.Name + " mesajın gönderildi.";
            return Page();
        }


    }
}