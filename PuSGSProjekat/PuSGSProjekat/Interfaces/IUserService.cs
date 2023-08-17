using PuSGSProjekat.DTO;
using PuSGSProjekat.DTO.LoginDTO;
using PuSGSProjekat.DTO.VerificationDTO;
using PuSGSProjekat.DTO.UserDTO;

using System.Collections.Generic;


namespace PuSGSProjekat.Interfaces
{
    public interface IUserService
    {
        List<UserResponseDTO> GetAllUsers();
        LoginResponseDTO LoginUser(LoginRequestDTO requestDto);
        UserResponseDTO UpdateUser(long id, UserEditRequestDTO requestDto);
        VerificationResponseDTO VerifyUser(long id, VerificationRequestDTO requestDto);
        UserResponseDTO RegisterUser(RegistrationRequestDTO requestDto);
        UserResponseDTO GetUserById(long id);
        
    }
}
