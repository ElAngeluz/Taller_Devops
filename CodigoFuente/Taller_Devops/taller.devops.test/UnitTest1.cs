namespace taller.devops.test
{
    public class UnitTest1
    {
        [Fact]
        public void SendNotification()
        {
            SendNotificationRepository SendTransaccion = new();


            var result = SendTransaccion.SendTransaccion(new application.models.dtos.notifications.DTOSendNotification() 
            {
                From = "Rita Asturia",
                Message = "This is a test",
                TimeToLifeSec = 45,
                To = "Juan Perez"
            });

            Assert.Equal("Hello Rita Asturia your message will be send", result.ToString());
        }
    }
}