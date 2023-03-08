using Microsoft.AspNetCore.Mvc;
using taller.devops.application.interfaces.services;
using taller.devops.application.models.dtos.notifications;

namespace taller.devops.api.Controllers.V1
{
    [Route("Devops")]
    [ApiController]
    [ApiVersion("1.0")]
    public class NotificationController : BaseApiController
    {
        private readonly ISendNotification ISendNotification;

        public NotificationController(ISendNotification iSendNotification)
        {
            ISendNotification = iSendNotification;
        }

        [HttpPost(Name = "Devops")]
        public ActionResult SendNotificacion(DTOSendNotification SendNotification)
        {
            var result = ISendNotification.SendTransaccion(SendNotification);

            return Ok(result);
        }
    }
}
