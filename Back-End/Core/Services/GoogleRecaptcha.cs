using Dietbox.ECommerce.Core.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dietbox.ECommerce.Core.Services
{
    public class GoogleRecaptcha
    {
        private readonly ISettings _settings;
        public GoogleRecaptcha(ISettings settings)
        {
            _settings = settings;
        }

        public async Task<bool> Validate(string recaptchaToken)
        {
            string secret = _settings.Recaptcha.Secret;
            string url = string.Format(_settings.Recaptcha.API, secret, recaptchaToken);
            HttpClient httpClient = new();
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
            if (httpResponse.IsSuccessStatusCode)
            {
                string data = await httpResponse.Content.ReadAsStringAsync();
                GoogleRecaptchaResponse? response = JsonConvert.DeserializeObject<GoogleRecaptchaResponse>(data);
                return response is null ? false : response.success;
            }
            else
            {
                return false;
            }      
        }


        #region Models JSON

        private class GoogleRecaptchaResponse
        {
            public bool success { get; set; }

            public DateTime challenge_ts { get; set; }

            public string hostname { get; set; }

            [JsonProperty("error-codes")]
            public List<object> errorcodes { get; set; }

        }

        #endregion

    }
}
