using System.Net;
using System.Net.Http;
using System.Web.Http;
using E_Guest.API.Hub;
using E_Guest.API.Infrastructure;
using E_Guest.API.Models;

namespace E_Guest.API.Controllers
{
    public class ChatController : BaseApiController
    { 
        public ChatController()
        {
        }
         
        [Route("api/Notification", Name = "SendNotification")]
        [HttpPost]
        public HttpResponseMessage SendNotification(UserHubModels obj)
        {
            ChatHub objChatHub = new ChatHub();
            obj.UserId = obj.UserId;
            obj.UserName = obj.UserName; 
            obj.Message= obj.Message;
             
            objChatHub.SendNotification(obj);
             

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
