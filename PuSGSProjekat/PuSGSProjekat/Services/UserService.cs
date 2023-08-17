using AutoMapper;
using PuSGSProjekat.Context;
using PuSGSProjekat.DTO.LoginDTO;
using PuSGSProjekat.DTO.UserDTO;
using PuSGSProjekat.DTO.VerificationDTO;
using PuSGSProjekat.Interfaces;
using System.Collections.Generic;

namespace PuSGSProjekat.Services
{
    public class UserService : IUserService
    {

        private readonly DatabaseContext _dbContext;
        //private readonly IMapper _mapper;

        public UserService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            //_mapper = mapper;
        }

        public List<UserResponseDTO> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public UserResponseDTO GetUserById(long id)
        {
            throw new System.NotImplementedException();
        }

        public LoginResponseDTO LoginUser(LoginRequestDTO requestDto)
        {
            throw new System.NotImplementedException();
        }

        public UserResponseDTO RegisterUser(RegistrationRequestDTO requestDto)
        {
            throw new System.NotImplementedException();
        }

        public UserResponseDTO UpdateUser(long id, UserEditRequestDTO requestDto)
        {
            throw new System.NotImplementedException();
        }

        public VerificationResponseDTO VerifyUser(long id, VerificationRequestDTO requestDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
