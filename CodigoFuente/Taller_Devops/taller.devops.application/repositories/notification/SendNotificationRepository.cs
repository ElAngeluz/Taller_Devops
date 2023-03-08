using taller.devops.application.interfaces.services;
using taller.devops.application.models.dtos.notifications;

namespace taller.devops.application.repositories.notification
{
    public class SendNotificationRepository : ISendNotification
    {
        public SendNotificationRepository()
        {

        }

        public object SendTransaccion(DTOSendNotification sendNotification)
        {
            try
            {
                return $"Hello {sendNotification.From} your message will be send";
            }
            catch (Exception EX)
            {
                throw new Exception("Error al enviar la trasacción.");

            }
        }
    }
}
