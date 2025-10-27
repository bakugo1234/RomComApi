using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RomCom.Model.ViewModels;
using System.Security.Claims;

namespace RomCom.Api.Controllers.Base
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[Action]")]
    public class BaseController : ControllerBase
    {
        protected AuthViewModel GetUser()
        {
            ClaimsPrincipal user = User;
            var userDataClaim = user.FindFirstValue(ClaimTypes.UserData);
            
            if (string.IsNullOrEmpty(userDataClaim))
            {
                return null;
            }

            var authViewModel = JsonConvert.DeserializeObject<AuthViewModel>(userDataClaim);
            return authViewModel;
        }

        protected int GetUserId()
        {
            var user = GetUser();
            return user?.id ?? 0;
        }

        protected string GetUserName()
        {
            var user = GetUser();
            return user?.userName ?? string.Empty;
        }
    }
}

