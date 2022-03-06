namespace Xpenser.API.Common
{
    public class EmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string CopyAddress { get; set; }
    }
}
