using CeoHelper.Shared.Options;
using CeoHelper.Web.Validators.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Net;

namespace CeoHelper.Web.Validators
{
    public class GoogleReCaptchaValidator : ICaptchaValidator
    {
        private readonly HttpClient _httpClient;
        private const string RemoteAddress = "https://www.google.com/recaptcha/api/siteverify";
        private string _secretKey;
        private readonly double _minimumScore;

        public GoogleReCaptchaValidator(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;           
            var googleReCaptchaSettings = configuration.GetSection(nameof(GoogleReCaptchaSettings));
            _secretKey = googleReCaptchaSettings.GetValue<string>(nameof(GoogleReCaptchaSettings.SecretKey));
            _minimumScore = googleReCaptchaSettings.GetValue<double>(nameof(GoogleReCaptchaSettings.MinimumScore));
        }

        public async Task<JObject> GetCaptchaResultDataAsync(string token)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _secretKey),
                new KeyValuePair<string, string>("response", token)
            });
            var res = await _httpClient.PostAsync(RemoteAddress, content);
            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException(res.ReasonPhrase);
            }
            var jsonResult = await res.Content.ReadAsStringAsync();
            return JObject.Parse(jsonResult);
        }

        public async Task<bool> IsCaptchaPassedAsync(string token)
        {
            dynamic response = await GetCaptchaResultDataAsync(token);
            if (response.success == "true")
            {
                return System.Convert.ToDouble(response.score) >= _minimumScore;
            }
            return false;
        }

        public void UpdateSecretKey(string key)
        {
            _secretKey = key;
        }
    }
}
