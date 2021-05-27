using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;
using BlazorPresentationServer.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace BlazorPresentationServer.Authentication
{
    public class CustomAuthenticationProvider: AuthenticationStateProvider
    {
        private readonly IJSRuntime jsRuntime;
        private readonly ILoginAccountService loginService;

        private Account cachedUser;

        public CustomAuthenticationProvider(IJSRuntime jsRuntime, ILoginAccountService loginService)
        {
            this.jsRuntime = jsRuntime;
            this.loginService = loginService;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (cachedUser == null)
            {
                string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    Account tmp = JsonSerializer.Deserialize<Account>(userAsJson);
                    ValidateLoginAsync(tmp);
                }
            }else
            {
                identity = SetupClaimsForUser(cachedUser);
            }

            ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
        }
        
        public async Task ValidateLoginAsync(Account account) {
            ClaimsIdentity identity = new ClaimsIdentity();
            try {
                Account user = await loginService.LoginAccountAsync(account);
                identity = SetupClaimsForUser(user);
                string serialisedData = JsonSerializer.Serialize(user);
                jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
                cachedUser = user;
            } catch (Exception e) {
                throw e;
            }

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }
        
        private ClaimsIdentity SetupClaimsForUser(Account user) {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Login", user.Login));
            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
            return identity;
        }
        
        public void Logout() {
            cachedUser = null;
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}