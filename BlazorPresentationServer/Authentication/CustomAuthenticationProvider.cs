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
        private readonly ICachedAccount CachedAccount;

        public Account cachedAccount { get; set; }

        public CustomAuthenticationProvider(IJSRuntime jsRuntime, ILoginAccountService loginService, ICachedAccount cash)
        {
            this.jsRuntime = jsRuntime;
            this.loginService = loginService;
            CachedAccount = cash;
            cachedAccount = CachedAccount.GetCachedAccount() ?? new Account();
            
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (cachedAccount == null) {
                string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentAccount");
                if (!string.IsNullOrEmpty(userAsJson)) {
                    Account tmp = JsonSerializer.Deserialize<Account>(userAsJson);
                    ValidateLoginAsync(tmp);
                }else {
                    identity = SetupClaimsForUser(cachedAccount);
                }
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
                cachedAccount = user;
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
            cachedAccount = null;
            CachedAccount.SetCachedAccount(null);
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentAccount", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}