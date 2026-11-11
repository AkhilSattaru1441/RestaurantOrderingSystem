using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderingAPI.Data;
using RestaurantOrderingAPI.Models;
using RestaurantOrderingAPI.DTOs.User;
using RestaurantOrderingAPI.Services;
using RestaurantOrderingAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantOrderingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
     private readonly RestaurantDbContext _context;
     private readonly IUserService _userService;
        public UsersController(RestaurantDbContext context,IUserService userService)
        {
                _context = context;
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            var response = users.Select(user => new UserResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RoleId = 2,
                CreatedAt = user.CreatedAt
            }).ToList();

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            var response = new UserResponseDto
            {
                UserId = users.UserId,
                UserName = users.UserName,
                Email = users.Email,
                PhoneNumber = users.PhoneNumber,
                RoleId = users.RoleId,
                CreatedAt = users.CreatedAt
            };

            return Ok(response);
        }

     
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateDto userDto)
        {
            var existingPhone = await _context.Users
                .FirstOrDefaultAsync(x => x.PhoneNumber == userDto.PhoneNumber);

            if (existingPhone != null)
            {
                return BadRequest("Phone number already exists.");
            }

            var existingEmail = await _context.Users
           .FirstOrDefaultAsync(x => x.Email == userDto.Email);

            if (existingEmail != null)
            {
                return BadRequest("Email already exists.");
            }
            var user = new User
        {
            UserName = userDto.UserName,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            RoleId = userDto.RoleId,
            CreatedAt = DateTime.Now
        };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
           
            var response = new UserResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RoleId = 2,
                CreatedAt = user.CreatedAt
            };

            return CreatedAtAction(
                nameof(GetUsers),
                new { id = response.UserId },
                response);
        }

        [Authorize(Roles = "Admin")]

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateDto userDto)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null) 
            {
                return NotFound();

            }

            existingUser.UserName = userDto.UserName;
            existingUser.Email = userDto.Email;
            existingUser.PhoneNumber = userDto.PhoneNumber;
            existingUser.RoleId = userDto.RoleId;

            await _context.SaveChangesAsync();

            var response = new UserResponseDto
            {
                UserId = existingUser.UserId,
                UserName = existingUser.UserName,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                RoleId = existingUser.RoleId,
                CreatedAt = existingUser.CreatedAt
            };
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();

            return Ok("User deleted successfully.");

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _userService.LoginAsync(loginDto);
            return Ok(new { Token = token });
        }
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var result = await _userService.ChangePasswordAsync(dto);

            if (!result)
                return BadRequest("Invalid email or current password.");

            return Ok("Password changed successfully.");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var result = await _userService.ForgotPasswordAsync(dto);

            if (!result)
                return BadRequest("User not found.");

            return Ok("Password reset successfully.");
        }
    }
}

