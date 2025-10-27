using System.Threading.Tasks;
using RomCom.Model.ViewModels;
using RomCom.Service.Data.Model.Contract;

namespace RomCom.Service.Services.Contracts
{
    public interface ILoginService
    {
        Task<IServiceResult> Login(UserCredentials credentials);
        Task<IServiceResult> RefreshToken(RefreshTokenModel model);
        Task<IServiceResult> ChangePassword(ChangePasswordViewModel model, string userName);
        Task<IServiceResult> Logout(int userId);
    }
}

