using Microsoft.AspNetCore.Mvc;
using taller.devops.application.models.dtos.notifications;

namespace taller.devops.api.Controllers.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class NotificationController : BaseApiController
    {
        [HttpPost(Name = "SendNotification")]
        public ActionResult SendNotificacion(DTOSendNotification SendNotification)
        {
            return Ok();
        }
    }
}
