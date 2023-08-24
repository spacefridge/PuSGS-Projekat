using AutoMapper;
using PuSGSProjekat.DTO.ArticleDTO;
using PuSGSProjekat.DTO.DeleteDTO;
using PuSGSProjekat.DTO.OrderDTO;
using PuSGSProjekat.DTO.UserDTO;
using PuSGSProjekat.DTO.VerificationDTO;
using PuSGSProjekat.Models;

namespace PuSGSProjekat.Mapper
{
    public class MapProfile : Profile
    {

        public MapProfile()
        {

  


            CreateMap<User, UserResponseDTO>();
            CreateMap<RegistrationRequestDTO, User>();
            CreateMap<UserEditRequestDTO, User>();
            CreateMap<User, VerificationResponseDTO>();

            CreateMap<VerificationRequestDTO, User>();

            CreateMap<ArticleRequestDTO, Article>();
            CreateMap<Article, ArticleResponseDTO>();
            CreateMap<Article, DeleteResponseDTO>();
          

            CreateMap<Order, DeleteResponseDTO>();
            CreateMap<Order, OrderResponseDTO>();
            CreateMap<OrderRequestDTO, Order>();
           

        }
    }
}
