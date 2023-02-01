using Newtonsoft.Json.Linq;

namespace CeoHelper.Web.Validators.Interfaces
{
    public interface ICaptchaValidator
    {
        Task<bool> IsCaptchaPassedAsync(string token);
        Task<JObject> GetCaptchaResultDataAsync(string token);
        void UpdateSecretKey(string key);
    }
}
