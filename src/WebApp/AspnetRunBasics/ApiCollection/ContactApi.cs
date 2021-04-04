using AspnetRunBasics.ApiCollection.Infrastructure;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using AspnetRunBasics.Settings;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspnetRunBasics.ApiCollection
{
    public class ContactApi : BaseHttpClientWithFactory, IContactApi
    {
        private readonly IApiSettings _settings;

        public ContactApi(IHttpClientFactory factory, IApiSettings settings)
            : base(factory)
        {
            _settings = settings;
        }
        public async Task<IEnumerable<ContactViewModel>> GetContact()
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                                        .SetPath(_settings.ContactPath)
                                        .HttpMethod(HttpMethod.Get)
                                        .GetHttpMessage();
            return await SendRequest<IEnumerable<ContactViewModel>>(message); ;
        }

        public async Task<IActionResult> CreateContact(ContactViewModel contactModel)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                                 .SetPath(_settings.ContactPath)
                                 .HttpMethod(HttpMethod.Post)
                                 .GetHttpMessage();

            var json = JsonConvert.SerializeObject(contactModel);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await SendRequest<IActionResult>(message);
        }

        public async Task<IActionResult> UpdateContact(ContactViewModel contactModel)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                               .SetPath(_settings.ContactPath)
                               .HttpMethod(HttpMethod.Put)
                               .GetHttpMessage();

            var json = JsonConvert.SerializeObject(contactModel);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await SendRequest<IActionResult>(message);
        }

        public async Task<IActionResult> DeleteContact(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                               .SetPath(_settings.ContactPath)
                               .HttpMethod(HttpMethod.Delete)
                               .AddToPath(id)
                               .GetHttpMessage();

            return await SendRequest<IActionResult>(message);
        }
    }
}
