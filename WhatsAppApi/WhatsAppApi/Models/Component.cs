using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsAppApi.Models
{
    public class Component
    {
        public string type { get; set; } = "body";
        public List<Parameter> parameters { get; set; }
    }
}
