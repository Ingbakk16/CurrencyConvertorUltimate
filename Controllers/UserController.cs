using CurrencyConverter2023.Data;
using CurrencyConverter2023.Entities;
using CurrencyConverter2023.Helpers;
using CurrencyConverter2023.Models.Dtos;
using CurrencyConverter2023.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter2023.Controllers

   
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ConvertorDbContext _dbContext;


        public UserController(IUserService userService, ConvertorDbContext dbContext)
        {
            _userService = userService;
            _dbContext = dbContext;
            
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegistrationDto registrationDto)
        {
            if (await _userService.IsUsernameTaken(registrationDto.UserName))
            {
                return BadRequest("Username is already taken.");
            }

            if (await _userService.IsEmailTaken(registrationDto.Email))
            {
                return BadRequest("Email is already taken.");
            }


            string hashedPassword = PasswordHasher.HashPassword(registrationDto.Password);

            // Create a new user entity
            var newUser = new User
            {
                UserName = registrationDto.UserName,
                Email = registrationDto.Email,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Password = hashedPassword


               
            };

            // Save the user to the database
            await _userService.CreateUser(newUser);

            // Return a success response
            return Ok("Registration successful.");
        }


       
        [HttpPut("upgrade-to-premium/{userId}")]
        public IActionResult UpgradeToPremium(int userId)
        {
            try
            {
                _userService.UpgradeToPremium(userId);
                return Ok("User upgraded to premium successfully");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }



    }
}
