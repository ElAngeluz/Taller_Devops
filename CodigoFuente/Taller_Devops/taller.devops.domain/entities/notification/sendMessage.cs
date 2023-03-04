namespace taller.devops.domain.entities.notification
{
    public class SendMessage
    {
        public int TimeToLifeSec { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
    }
}
