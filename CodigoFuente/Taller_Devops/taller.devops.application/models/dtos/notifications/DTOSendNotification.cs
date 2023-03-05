namespace taller.devops.application.models.dtos.notifications
{
    public class DTOSendNotification
    {
        public int TimeToLifeSec { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
    }
}
