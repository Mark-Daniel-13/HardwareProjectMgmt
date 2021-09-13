using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebPOS.BlazorUI.Auth;
using System.Text.Json;
using System.Net.Http.Headers;

namespace WebPOS.BlazorUI.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }
        public async Task<AuthUserModel> Login(LoginUserModel loginModel)
        {
            using (var userDb = new Business.Users.Facades.User())
            {
                var validateData = await userDb.ValidateUser(loginModel.Username, loginModel.Password);
                if (validateData == null)
                {
                    return null;
                }
                var decerializedData = AuthUserModel.ToModel(validateData);
                await _localStorage.SetItemAsync(Business.Globals.TokenName, decerializedData.AccessToken);
                ((StateProvider)_authStateProvider).NotifyUserAuthentication(decerializedData.AccessToken);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", decerializedData.AccessToken);
                return decerializedData;
            }
        }
        public async Task LogOut()
        {
            await _localStorage.RemoveItemAsync(Business.Globals.TokenName);
            ((StateProvider)_authStateProvider).NotifyUserLogOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
