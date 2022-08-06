namespace FocoTest.Helpers.SMS
{
    public class SmsClient
    {
        private string accountUsername;
        private string accountPassword;

        public SmsClient(string accountUsername, string accountPassword)
        {
            this.accountUsername = accountUsername;
            this.accountPassword = accountPassword;
        }

        public string SendMessage(string mobileNo, string message, DateTime scheduleTime)
        {
            string result = "";
            return result;
        }
    }
}
