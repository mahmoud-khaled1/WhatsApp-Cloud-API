using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatsAppApi.Settings;
using WhatsAppApi.Dtos;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using WhatsAppApi.Models;
using System.Net.Http.Json;
using WhatsAppApi.Repository;

namespace WhatsAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IWhatsAppService _whatsAppService;

        public AuthController(IWhatsAppService whatsAppService)
        {
            _whatsAppService = whatsAppService;
        }

        [HttpPost("send-welcome-message")]
        public async Task<IActionResult> SendWelcomeMessage(SendMessageDto dto)
        {
            //var language = Request.Headers["language"].ToString();
            var language = "en_US";
            var result = await _whatsAppService.SendMessage(dto.Mobile, language, "hello_world");

            if (!result)
                throw new Exception("Something went wrong!");

            return Ok("Sent successfully");
        }

        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOTP(SendOTPDto dto)
        {
            //var language = Request.Headers["language"].ToString();
            var language = "en_US";
            Random random = new();
            var otp = random.Next(0, 999999); //to get random number of 6 digit as otp

            var components = new List<Component>
            {
                new Component
                {
                     type="body",
                     parameters=new List<Parameter>
                     {
                          new Parameter{type="text",text=dto.Name},
                          new Parameter{type="text",text=otp.ToString()}
                      }
                }
            };
            var result = await _whatsAppService.SendMessage(dto.Mobile, language, "send_otp_new", components);

            if (!result)
                throw new Exception("Something went wrong!");

            return Ok("Sent successfully");
        }

        [HttpPost("send-order-track-details")]
        public async Task<IActionResult>OrderStaus(SendOrderTrackImage dto,string Mobile)
        {
            var language = "en_US";
           
            var components = new List<Component>
            {
                new Component
                {
                     type="header",
                     parameters=new List<Parameter>
                     {
                          new Parameter{type="image",image=new Image{
                            link=dto.Link
                          } 
                          },
                         
                      }
                }
            };
            var result = await _whatsAppService.SendMessage(Mobile, language, "orde_status", components);

            if (!result)
                throw new Exception("Something went wrong!");

            return Ok("Sent successfully");
        }
    }
}

// link of WhatsApp Cloud API :https://developers.facebook.com/?no_redirect=1
// link Message Templates :https://developers.facebook.com/docs/whatsapp/cloud-api/guides/send-message-templates
// Body of the request like  :
//{
//  "messaging_product": "whatsapp",
//  "recipient_type": "INDIVIDUAL",
//  "to": "201020795015",
//  "type": "template",
//  "template": {
//        "name": "send_otp",
//    "language": {
//            "code": "en_US"
//    },
//    "components": [
//      {
//            "type": "body",
//        "parameters": [
//          {
//                "type": "text",
//            "text": "mahmoudAly"
//          },
//           {
//                "type": "text",
//            "text": "987541"
//          }
//        ]
//      }
//    ]
//  }
//}