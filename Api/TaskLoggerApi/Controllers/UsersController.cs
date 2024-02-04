using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskLoggerApi.Interfaces;
using TaskLoggerApi.Models.User;

namespace TaskLoggerApi.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReturnDTO>>> GetUsers()
        {
            var user = await _userRepository.GetUsersAsync();

            var userToReturn = _mapper.Map<IEnumerable<UserReturnDTO>>(user);

            return Ok(userToReturn);
        }

        // GET: api/Users/5
        [HttpGet("{username}")]
        public async Task<ActionResult<UserReturnDTO>> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUserNameAsync(username);

            var userToReturn = _mapper.Map<UserReturnDTO>(user);

            return Ok(userToReturn);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserDTO updateUser)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await _userRepository.GetUserByUserNameAsync(username);

            if (user == null) return NotFound();

            _mapper.Map(updateUser,user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("User could not be updated");
        }
    }
}
