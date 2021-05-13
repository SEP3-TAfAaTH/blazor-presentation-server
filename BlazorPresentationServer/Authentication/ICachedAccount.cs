using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Authentication
{
    public interface ICachedAccount
    {
        Account GetCachedAccount();

        void SetCachedAccount(Account account);
    }
}