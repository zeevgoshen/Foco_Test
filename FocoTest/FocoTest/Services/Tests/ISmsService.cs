namespace FocoTest.Services.Tests
{
    public interface ISmsService
    {
        void SendSmsMessage(string phoneNumber, string message);
    }
}
