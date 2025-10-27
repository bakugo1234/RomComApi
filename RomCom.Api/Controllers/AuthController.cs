using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RomCom.Api.Controllers.Base;
using RomCom.Model.DTOs.Auth.Requests;
using RomCom.Service.Services.Contracts;
using RomCom.Repository.Setup.Contract;

namespace RomCom.Api.Controllers
{
    public class AuthController : BaseController
    {
        private readonly ILoginService _loginService;
        private readonly IConnectionProvider _connectionProvider;

        public AuthController(ILoginService loginService, IConnectionProvider connectionProvider)
        {
            _loginService = loginService;
            _connectionProvider = connectionProvider;
        }

        /// <summary>
        /// Login endpoint for user authentication
        /// </summary>
        /// <param name="credentials">User credentials (username/email and password)</param>
        /// <returns>JWT token and user information</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto credentials)
        {
            var result = await _loginService.Login(credentials);
            return Ok(result);
        }

        /// <summary>
        /// Refresh token endpoint
        /// </summary>
        /// <param name="model">Refresh token</param>
        /// <returns>New JWT token</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto model)
        {
            var result = await _loginService.RefreshToken(model);
            return Ok(result);
        }

        /// <summary>
        /// Change password for authenticated user
        /// </summary>
        /// <param name="model">Password change details</param>
        /// <returns>Success status</returns>
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto model)
        {
            var userName = GetUserName();
            var result = await _loginService.ChangePassword(model, userName);
            return Ok(result);
        }

        /// <summary>
        /// Logout endpoint
        /// </summary>
        /// <returns>Success status</returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var userId = GetUserId();
            var result = await _loginService.Logout(userId);
            return Ok(result);
        }

        /// <summary>
        /// Get current authenticated user information
        /// </summary>
        /// <returns>User information</returns>
        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            var user = GetUser();
            
            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }

        /// <summary>
        /// Health check endpoint to test database connection
        /// </summary>
        /// <returns>Database connection status</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("health/database")]
        public IActionResult CheckDatabaseConnection()
        {
            try
            {
                using var connection = _connectionProvider.GetOpenDbConnection();
                
                // Test the connection with a simple query
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM [auth].[tbl_users]";
                var userCount = command.ExecuteScalar();
                
                // Also test if we can get table structure
                command.CommandText = "SELECT TOP 1 * FROM [auth].[tbl_users]";
                using var reader = command.ExecuteReader();
                var hasData = reader.HasRows;
                reader.Close();
                
                return Ok(new { 
                    status = "success", 
                    message = "Database connection is working", 
                    timestamp = System.DateTime.UtcNow,
                    userCount = userCount,
                    hasData = hasData,
                    connectionString = connection.ConnectionString.Split(';')[0] // Show only server info for security
                });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { 
                    status = "error", 
                    message = "Database connection failed", 
                    error = ex.Message,
                    timestamp = System.DateTime.UtcNow
                });
            }
        }
    }
}

