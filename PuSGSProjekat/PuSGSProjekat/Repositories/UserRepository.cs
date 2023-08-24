using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using PuSGSProjekat.Context;
using PuSGSProjekat.DTO.LoginDTO;
using PuSGSProjekat.DTO.UserDTO;
using PuSGSProjekat.DTO.VerificationDTO;
using PuSGSProjekat.ExceptionHandler;
using PuSGSProjekat.Interfaces.Repositories;
using PuSGSProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PuSGSProjekat.Repositories
{
    public class UserRepository : IUserRepositories
    {

        private readonly DatabaseContext _dbContext;
        public UserRepository(DatabaseContext databaseContext) 
        {
            _dbContext = databaseContext;
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();      
        }

        public User GetUserByID(long id)
        {
            return _dbContext.Users.Find(id);
        }

        public User LoginUser(LoginRequestDTO requestDto)
        {
           return _dbContext.Users.FirstOrDefault(u => u.Email == requestDto.Email);
        }

        public void RegisterUser(User user)
        {
            _dbContext.Users.Add(user);             
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public User UpdateUser(long id, UserEditRequestDTO requestDto)
        {
            throw new System.NotImplementedException();
        }

        public User VerifyUser(long id, VerificationRequestDTO requestDto)
        {
            throw new System.NotImplementedException();
        }
    }
}
