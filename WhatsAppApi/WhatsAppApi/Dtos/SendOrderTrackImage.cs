using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsAppApi.Dtos
{
    public class SendOrderTrackImage
    {
        public string type { get; set; } = "image";
        public string Link { get; set; }
    }
}
