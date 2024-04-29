using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HIMAIKFinanceMonitorApp.Server.Data;
using HIMAIKFinanceMonitorApp.Server.Models;
using HIMAIKFinanceMonitorApp.Server.Dtos;
using HIMAIKFinanceMonitorApp.Server.Helpers;
using AutoMapper;

namespace HIMAIKFinanceMonitorApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IConfiguration config) : ControllerBase
    {

        private readonly DataContext _context = new(config);

        private readonly IConfiguration _config = config;

        private readonly UserHelper _userHelper = new();

        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.CreateMap<UserRegistrationDto, User>();
        }));
    

        [HttpGet("test")]
        public async Task<DateTime> TestConnection ()
        {
            using var command = _context.Database.GetDbConnection().CreateCommand();
            {
                command.CommandText = "SELECT GETDATE()";
                await _context.Database.OpenConnectionAsync();

                using (var result = await command.ExecuteReaderAsync())
                {
                    if (result.Read())
                    {
                        return (DateTime)result[0];
                    }
                }
            }

            throw new Exception("Unable to get date from SQL Server");
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult<User>> RegisterUser(UserRegistrationDto userDto)
        {
            if (userDto.Password != userDto.ConfirmPassword)
            {
                return BadRequest("Passwords do not match");
            }

            if (_context.Users.Any(u => u.Username == userDto.Username))
            {
                return BadRequest("Username already exists");
            }



            var user = _mapper.Map<User>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            user.CreatedAt = DateTime.Now;

            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<string>> LoginUser(UserLoginDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userDto.Username);

            if (user == null)
            {
                return NotFound("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.Password))
            {
                return Unauthorized("Invalid password");
            }

            var token = _userHelper.GenerateJwtToken(user.UserId, _config);
            return Ok(new { token });
        }

        [HttpPut("ResetPassword")]
        public async Task<ActionResult<User>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto.Password != resetPasswordDto.ConfirmPassword)
            {
                return BadRequest("Passwords do not match");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == resetPasswordDto.Username);

            if (user == null)
            {
                return NotFound("User not found");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.Password);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Failed To Update Password");
            }

            return Ok("Reset Password Successful");
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {

            if (id != user.UserId)
            {
                return NotFound("User Not Found");
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(e => e.UserId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Update User Success");
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
