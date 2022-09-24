using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WhatsAppApi.Models;

namespace WhatsAppApi.Repository
{
    public interface IWhatsAppService
    {
        Task<bool> SendMessage(string mobile, string language, string template, List<Models.Component>? components = null);
    }
}
