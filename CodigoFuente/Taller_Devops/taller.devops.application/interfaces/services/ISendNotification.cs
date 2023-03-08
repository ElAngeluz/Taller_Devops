using taller.devops.application.models.dtos.notifications;

namespace taller.devops.application.interfaces.services
{
    public interface ISendNotification
    {
        object SendTransaccion(DTOSendNotification sendNotification);
    }
}