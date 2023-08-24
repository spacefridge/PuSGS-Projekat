using AutoMapper;
using EntityFramework.Exceptions.Common;
using Microsoft.IdentityModel.Tokens;
using PuSGSProjekat.Context;
using PuSGSProjekat.DTO.LoginDTO;
using PuSGSProjekat.DTO.UserDTO;
using PuSGSProjekat.DTO.VerificationDTO;
using PuSGSProjekat.Enumerations;
using PuSGSProjekat.ExceptionHandler;
using PuSGSProjekat.Interfaces;
using System.Security.Claims;
using PuSGSProjekat.Interfaces.Repositories;
using PuSGSProjekat.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace PuSGSProjekat.Services
{
    public class UserService : IUserService
    {

    
        private readonly IMapper _mapper;
        private readonly IUserRepositories _userRepositories;
        private readonly IConfigurationSection _secretKey;

        public UserService(IMapper mapper, IUserRepositories userRepositories, IConfiguration config)
        {

            _mapper = mapper;
            _userRepositories = userRepositories;
            _secretKey = config.GetSection("SecretKey");
        }

        public UserResponseDTO RegisterUser(RegistrationRequestDTO requestDto)
        {
            User user = _mapper.Map<User>(requestDto);

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt());
            user.VerificationState = user.UserType == UserType.Seller ? VerificationState.Pending : null;

            _userRepositories.RegisterUser(user);

            try
            {
                _userRepositories.SaveChanges();
            }
            catch (UniqueConstraintException)
            {
                throw new InvalidCredentials("Username and/or Email is taken.");
            }
            catch (CannotInsertNullException)
            {
                throw new InvalidField("Missing field/s.");
            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.Map<UserResponseDTO>(user);
        }

        public LoginResponseDTO LoginUser(LoginRequestDTO requestDto)
        {
            User user = _userRepositories.LoginUser(requestDto);

            if (user == null)
            {
                throw new InvalidCredentials("Invalid Credentials.");
            }

            if (!BCrypt.Net.BCrypt.Verify(requestDto.Password, user.Password))
            {
                throw new InvalidCredentials("Invalid Credentials.");
            }

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.UserType.ToString()));
            if (user.UserType == UserType.Seller && user.VerificationState == VerificationState.Accepted)
            {
                claims.Add(new Claim("VerificationState", user.VerificationState.ToString()));
            }

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));

            SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: "http://localhost:44379",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signingCredentials
            );

            LoginResponseDTO responseDto = new LoginResponseDTO()
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
            };

            return responseDto;
        }

        public UserResponseDTO UpdateUser(long id, UserEditRequestDTO requestDto)
        {
            User user = _userRepositories.GetUserByID(id);

            if (user == null)
            {
                throw new ResourceMissing("No user with given id.");
            }

            _mapper.Map(requestDto, user);

            try
            {
                _userRepositories.SaveChanges();
            }
            catch (UniqueConstraintException)
            {
                throw new InvalidCredentials("Username and/or Email is taken.!");
            }
            catch (CannotInsertNullException)
            {
                throw new InvalidField("Missing field/s.");
            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.Map<UserResponseDTO>(user);
        }

        public VerificationResponseDTO VerifyUser(long id, VerificationRequestDTO requestDto)
        {
            User user =_userRepositories.GetUserByID(id);

            if (user == null)
            {
                throw new ResourceMissing("No user with given id.");
            }

            if (user.UserType != UserType.Seller)
            {
                throw new InvalidField("Not a seller. Can't verify.");
            }

            _mapper.Map(requestDto, user);

            try
            {
                _userRepositories.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }

            return _mapper.Map<VerificationResponseDTO>(user);
        }

        public List<UserResponseDTO> GetAllUsers()
        {
            return _mapper.Map<List<UserResponseDTO>>(_userRepositories.GetAllUsers());
        }

        public UserResponseDTO GetUserById(long id)
        {
            UserResponseDTO user = _mapper.Map<UserResponseDTO>(_userRepositories.GetUserByID(id));

            if (user == null)
            {
                throw new ResourceMissing("No user with given id.");
            }

            return user;
        }
    }
}
