using Blazored.LocalStorage;
using CSV.Interfaces;
using System.Net.Http.Json;
using CSV.Models.User;
using Microsoft.AspNetCore.Components.Authorization; // Add this for AuthenticationStateProvider
using System.Security.Claims; // Add this if you're working with Claims
using System.Net.Http; // Ensure this is present for HttpClient
using System.Threading.Tasks;
using System.Text.Json; // Ensure this is present for Task

namespace CSV.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task Login(UserLoginDto userLoginDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", userLoginDTO);
            response.EnsureSuccessStatusCode();

            var user = await response.Content.ReadAsStringAsync();

            var userToUse = JsonSerializer.Deserialize<UserLoggedInDto>(user);

            

            if (_authenticationStateProvider is ApiAuthenticationStateProvider authStateProvider)
            {
                if (userToUse == null || userToUse.Token == null || userToUse.Token == string.Empty)
                {
                    await _localStorage.RemoveItemAsync("authToken");
                    return;
                }

                await _localStorage.SetItemAsync("authToken", userToUse.Token);
                authStateProvider.MarkUserAsAuthenticated(userToUse.Token);
            }

        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            if (_authenticationStateProvider is ApiAuthenticationStateProvider authStateProvider)
            {
                authStateProvider.MarkUserAsLoggedOut();
            }
        }
    }
}
