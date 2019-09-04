using System;
using Newtonsoft.Json;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Token
{
    public class TokenResponseDto
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = ".issued")]
        public DateTime Issued { get; set; }

        [JsonProperty(PropertyName = ".expires")]
        public DateTime Expires { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}