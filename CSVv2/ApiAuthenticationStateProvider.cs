using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace CSVv2
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            
                string token = await _localStorage.GetItemAsStringAsync("authToken");

                var identity = new ClaimsIdentity();
                if (!string.IsNullOrEmpty(token))
                {
                    // Set the token on the outgoing request headers
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
                }

                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
           
            
        }

        public void MarkUserAsAuthenticated(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null)
            {
                // Attempt to retrieve the NameIdentifier claim value
                if (keyValuePairs.TryGetValue(ClaimTypes.NameIdentifier, out object nameIdentifier) && nameIdentifier != null)
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier.ToString()));
                }

                // Add other claims as needed for your application
                // Make sure to check for nulls and existence in the dictionary before adding them
            }

            return claims;
        }


        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            base64 = base64.Replace('_', '/').Replace('-', '+');
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }

}
