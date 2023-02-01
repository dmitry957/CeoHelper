namespace CeoHelper.Shared.Options
{
    public class GoogleReCaptchaSettings
    {
        public string SiteKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public double MinimumScore { get; set; }
    }
}
