using System.Threading.Tasks;

namespace WebPOS.BlazorUI.Auth
{
    public interface IAuthService
    {
        Task<AuthUserModel> Login(LoginUserModel loginModel);
        Task LogOut();
    }
}