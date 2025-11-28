using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAAI_TW_01.Models.GoogleRe
{
    public class ReCaptchaResponse
    {
        // ReCaptcha response from Google
        //https://developers.google.com/recaptcha/docs/verify

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("challenge_ts")]
        public DateTime ChallengeTime { get; set; }  // optional

        [JsonProperty("hostname")]
        public string Hostname { get; set; }  // optional

        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }  // optional
    }
}