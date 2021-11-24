using AutoMapper;
using GestionContact.Helpers;
using GestionContact.Models;
using GestionContact.ParametersModels;
using GestionContact.ViewModels;
using GestionContactEF.Core.Interfaces;
using GestionContactEF.Dal.Models;
using GestionContactEF.Dal.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GestionContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository repo, IMapper mapper, UserManager<User> userManager, ITokenService tokenService)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        // GET: api/User
        [HttpGet]
        //documentation pour swagger
        [Produces("application/json", Type = typeof(IEnumerable<UserApi>))]
        //authorisation basée sur le role
        [AuthRequired("SuperAdmin")]
        public async Task<IActionResult> Get([FromQuery] GetUserParameters parameters)
        {
            try
            {
                IEnumerable<User> user = await _repo.Get(parameters.LazyLoading, parameters.Lastname,
                     parameters.Firstname);
                if (user == null)
                {
                    return NotFound();
                }
                IEnumerable<UserApi> userApis = _mapper.Map<IEnumerable<UserApi>>(user);

                foreach (UserApi userApi in userApis)
                {
                    User tmpUser = await _userManager.FindByNameAsync(userApi.UserName);
                    userApi.Roles = await _userManager.GetRolesAsync(tmpUser);
                    userApi.Password = "*******";
                }

                return Ok(userApis);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return NotFound();
        }

        // GET api/User/5
        [HttpGet("{id}")]
        [Produces("application/json", Type = typeof(UserApi))]
        [AuthRequired("SuperAdmin")]
        public async Task<IActionResult> Get(string id, [FromQuery] GetUserParameters parameters)
        {
            try
            {
                User tmpUser = await _repo.GetOne(id, parameters.LazyLoading);
                if (tmpUser == null)
                {
                    return NotFound();
                }
                UserApi tmpUserApi = _mapper.Map<User, UserApi>(tmpUser);


                tmpUserApi.Password = "*******";
                return Ok(tmpUserApi);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return NotFound();
        }

        // PUT api/User/5
        [HttpPut("{id}")]
        [Produces("application/json", Type = null)]
        public async Task<IActionResult> Put(string id, [FromBody] UserApi value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!id.Equals(value.Id))
            {
                return BadRequest();
            }

            User tmpUser = await _userManager.FindByIdAsync(id);
            if(tmpUser == null)
            {
                return NotFound();
            }
            

            try
            {
                UserApi user = _mapper.Map<UserApi>(tmpUser);
                var hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(null, value.Password);
                IdentityResult result = await _userManager.UpdateAsync(tmpUser);
                
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
            return NoContent();
        }

        // DELETE api/User/5
        [HttpDelete("{id}")]
        [Produces("application/json", Type = null)]
        [AuthRequired("SuperAdmin")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _repo.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }

            return NoContent();
        }

        [HttpGet]
        [Route("GetPagination")]
        public async Task<IActionResult> GetPagination([FromQuery] Parameters productParameters)
        {
            var users = await _repo.GetUsers(productParameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(users.MetaData));
            return Ok(users);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] ViewLoginApi loginDTOApi)
        {
            LoginDto loginDTO = _mapper.Map<ViewLoginApi, LoginDto>(loginDTOApi);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User tmpUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            bool isPasswordValid = await _userManager.CheckPasswordAsync(tmpUser, loginDTO.Password);
            if (tmpUser != null && isPasswordValid)
            {

                LoginSuccessDto loginSuccessDTO = new LoginSuccessDto();
                var roles = await _userManager.GetRolesAsync(tmpUser);
                List<Claim> authClaims = new List<Claim>();
                authClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, tmpUser.UserName));
                authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                foreach (var role in roles)
                {
                    authClaims.Add(new Claim("role", role));
                    loginSuccessDTO.Role = role;
                }
                loginSuccessDTO.Id = tmpUser.Id;
                loginSuccessDTO.Email = tmpUser.Email;
                loginSuccessDTO.Token = _tokenService.GenerateToken(loginSuccessDTO);
                loginSuccessDTO.ExpirationDate = DateTime.Now.AddDays(1);

                return Ok(loginSuccessDTO);
            }
            return BadRequest("mot de passe ou email incorrect");
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] ViewRegisterApi modelApi)
        {
            RegisterDto model = _mapper.Map<ViewRegisterApi, RegisterDto>(modelApi);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User tmpUser = await _userManager.FindByEmailAsync(model.Email);
            if (tmpUser == null)
            {
                User createdUser = new User
                {
                    Email = model.Email,
                    FirstName = model.Firstname,
                    LastName = model.Lastname,
                    EmailConfirmed = true
                };
                IdentityResult res = _userManager.CreateAsync(createdUser, model.Password).Result;
                _userManager.AddToRoleAsync(createdUser, model.Role).Wait();
                if (res.Succeeded)
                {
                    return Created(createdUser.Id, new RegisterSuccessDto
                    {
                        Id = createdUser.Id,
                        Email = createdUser.Email
                    });
                }
                else
                {
                    return StatusCode(500);
                }
            }
            else
            {
                ModelState.AddModelError("Already existing user", "A user with this email/username already exists.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("available")]
        public async Task<IActionResult> Available([FromBody] ViewLoginApi user)
        {
            var swAvailable = await _repo.UserMailExists(user.Email);
            return Ok(!swAvailable);
        }
    }
}
