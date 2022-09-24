using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WhatsAppApi.Models;
using WhatsAppApi.Settings;

namespace WhatsAppApi.Repository
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly WhatsAppSettings _settings;

        public WhatsAppService(IOptions<WhatsAppSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> SendMessage(string mobile, string language, string template, List<Component>? components = null)
        {
            using HttpClient httpClient = new();

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _settings.Token);

            WhatsAppRequest body = new()
            {
                to = mobile,
                template = new Template
                {
                    name = template,
                    language = new Language { code = language }
                }
            };

            if (components is not null)
                body.template.components = components;

            HttpResponseMessage response =
                await httpClient.PostAsJsonAsync(new Uri(_settings.ApiUrl), body);

            return response.IsSuccessStatusCode;
        }
    }
}
