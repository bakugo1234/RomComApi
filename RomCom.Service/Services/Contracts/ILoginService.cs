using System.Threading.Tasks;
using RomCom.Model.DTOs.Auth.Requests;
using RomCom.Service.Data.Model.Contract;

namespace RomCom.Service.Services.Contracts
{
    public interface ILoginService
    {
        Task<IServiceResult> Login(LoginRequestDto credentials);
        Task<IServiceResult> RefreshToken(RefreshTokenRequestDto model);
        Task<IServiceResult> ChangePassword(ChangePasswordRequestDto model, string userName);
        Task<IServiceResult> Logout(int userId);
    }
}

