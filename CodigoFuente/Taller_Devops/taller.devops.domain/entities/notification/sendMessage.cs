﻿namespace taller.devops.domain.entities.notification
{
    public class SentMesseage
    {
        public Guid Id { get; set; }
        public int TimeToLifeSec { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }
    }
}
