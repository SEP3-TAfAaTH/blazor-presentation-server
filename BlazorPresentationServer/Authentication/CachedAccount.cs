using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Authentication
{
    public class CachedAccount : ICachedAccount
    {
        public Account cachedAccount { get; set; }
        
        public Account GetCachedAccount()
        {
            return cachedAccount;
        }

        public void SetCachedAccount(Account account)
        {
            cachedAccount = account;
        }
    }
}