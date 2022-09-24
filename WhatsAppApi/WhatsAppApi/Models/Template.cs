using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsAppApi.Models
{
    public class Template
    {
        public string name { get; set; }
        public Language language { get; set; }
        public List<Component> components { get; set; }
    }
}
