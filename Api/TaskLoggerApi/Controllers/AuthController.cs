using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using TaskLoggerApi.Data;
using TaskLoggerApi.Interfaces;
using TaskLoggerApi.Models.User;

namespace TaskLoggerApi.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(UserManager<AppUser> userManager, ITokenService tokenService,IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _userManager = userManager;
        }

        //POST: api/auth/register
        [HttpPost("register")]
        public async Task<ActionResult<UserReturnDTO>> Register(RegisterUserDTO registerUser)
        {
            if (await UserExists(registerUser.UserName.ToLower())) return BadRequest("Username already Exists");


            var user = _mapper.Map<AppUser>(registerUser);

            user.UserName = registerUser.UserName.ToLower();
            var res = await _userManager.CreateAsync(user,registerUser.Password);

            if (!res.Succeeded) return BadRequest(res.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);
            return Ok(new UserReturnDTO
            {
                UserName = user.UserName,
                Token= await _tokenService.CreateToken(user)
            });
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserReturnDTO>> Login(UserLoginDTO userLoginDTO)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == userLoginDTO.UserName);

            if (user == null) return Unauthorized("Username incorrect");

            var res = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password);

            if (!res) return Unauthorized("Invalid Password");

            return Ok(new UserReturnDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            });

        }
        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}
