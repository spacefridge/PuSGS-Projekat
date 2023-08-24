using PuSGSProjekat.DTO.LoginDTO;
using PuSGSProjekat.DTO.UserDTO;
using PuSGSProjekat.DTO.VerificationDTO;
using PuSGSProjekat.Models;
using System.Collections.Generic;

namespace PuSGSProjekat.Interfaces.Repositories
{
    public interface IUserRepositories
    {
        List<User> GetAllUsers();
        User GetUserByID(long id);

        User LoginUser (LoginRequestDTO requestDto);

        void RegisterUser(User user);

        void SaveChanges();

        User UpdateUser(long id, UserEditRequestDTO requestDto);

        User VerifyUser(long id, VerificationRequestDTO requestDto);
    }
}
