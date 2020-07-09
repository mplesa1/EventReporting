using AutoMapper;
using EventReporting.Model.User;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.DataTransferObjects.User;
using EventReporting.Shared.Infrastructure.Constants;
using EventReporting.Shared.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtConstants = EventReporting.Shared.Infrastructure.Constants.JwtConstants;

namespace EventReporting.BusinessLayer.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private IConfiguration _config;

        public UserService(UserManager<User> userManager,
            IUserRepository userRepository,
            IConfiguration config,
            IMapper mapper) : base(mapper)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<ICollection<UserDto>> FindAllAsync()
        {
            var users = await _userRepository.FindAllAsync();
            var userDtos = Map<ICollection<User>, ICollection<UserDto>>(users);

            return userDtos;
        }

        public async Task<UserDto> FindByIdAsync(int id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            var userDto = Map<User, UserDto>(user);

            return userDto;
        }

        public async Task<UserData> LoginAsync(LoginDto loginDto)
        {
            var userToVerify = await _userManager.Users.Include(u => u.UserRoles)
                                                       .ThenInclude(u => u.Role)
                                                       .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (userToVerify == null)
            {
                throw new AuthenticationException("User does not exsist");
            }

            if (await _userManager.CheckPasswordAsync(userToVerify, loginDto.Password))
            {
                return await GetUserData(userToVerify);
            }

            throw new AuthenticationException("User credentials incorrect");
        }

        public async Task RegistrationAsync(RegistrationDto registrationDto)
        {
            User user = await _userRepository.FindByEmailOrPin(registrationDto.Email, registrationDto.PIN);
            if (user != null)
            {
                if (user.NormalizedEmail == registrationDto.Email.ToUpper())
                {
                    throw new BusinessException("Email exist");
                }

                if (user.PIN == registrationDto.PIN)
                {
                    throw new BusinessException("Pin exist");
                }
            }

            User userToRegister = new User
            {
                Email = registrationDto.Email,
                UserName = registrationDto.Email,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                PIN = registrationDto.PIN,
                PasswordHash = registrationDto.Password
            };

            userToRegister.UserRoles = new List<UserRole>
            {
                new UserRole
                {
                    RoleId = RoleConstants.USER_ID
                }
            };

            IdentityResult result = await _userManager.CreateAsync(userToRegister, userToRegister.PasswordHash);

            if (!result.Succeeded)
            {
                throw new BusinessException("Registration failed");
            }
        }

        #region JWT GENERATION

        private async Task<UserData> GetUserData(User user)
        {
            UserData userData = new UserData()
            {
                Email = user.Email,
                UserId = user.Id
            };

            var token = await GenerateJwt(user);
            userData.Token = token;

            return userData;
        }

        private async Task<string> GenerateJwt(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtConstants.ID_CLAIM_NAME, user.Id.ToString()),
                new Claim(JwtConstants.PIN_CLAIM_NAME, user.PIN),
                new Claim(JwtConstants.FIRST_NAME_CLAIM_NAME, user.FirstName),
                new Claim(JwtConstants.LAST_NAME_CLAIM_NAME, user.LastName),
                new Claim(JwtConstants.EMAIL_CLAIM_NAME, user.Email),
                new Claim(JwtConstants.ROLE_CLAIM_NAME, user.UserRoles.FirstOrDefault().Role.Name)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(600),
                signingCredentials: credentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

            return encodedJwt;
        }
        #endregion
    }
}
