namespace WRP3.Infrastructure.GoogleRecaptcha.Models
{
    public class GoogleRecaptchaSettings
    {
        public const string GoogleRecaptchaSetting = "GoogleRecaptchaSettings";
        public string? SiteKey { get; set; }
        public string? SecretKey { get; set; }
    }
}
