using taller.devops.application.interfaces.services;
using taller.devops.application.models.dtos.notifications;

namespace taller.devops.application.repositories.notification
{
    public class SendNotificationRepository : ISendNotification
    {
        public SendNotificationRepository()
        {

        }

        public (bool TrxExistosa, object Data) SendTransaccion(DTOSendNotification sendNotification)
        {
            try
            {

            }
            catch (Exception EX)
            {
                throw new Exception("Error al enviar la trasacción.");

            }
        }
    }
}
