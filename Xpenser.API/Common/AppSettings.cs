namespace Xpenser.API.Common
{
    public class AppSettings
    {
        public string WebAppBaseUrl { get; set; }
        public string SiteUrl { get; set; }
        public string SiteLogoUrl { get; set; }
        public string HelpUrl { get; set; }
        public string AppName { get; set; }
        public string CopyRightMessage { get; set; }
        public int FreeResumeCount { get; set; }
        public EmailSettings EmailOptions { get; set; }
    }
}
