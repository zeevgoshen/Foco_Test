
using FocoTest.Helpers;

namespace FocoTest.Services.Tests;


public class SmsService : ISmsService
{
    public void SendSmsMessage(string phoneNumber, string message)
    {
        SmsSender.SendMessageAsync("account", "password", phoneNumber, message);

    }
}
