using PuSGSProjekat.Context;
using PuSGSProjekat.DTO.ArticleDTO;
using PuSGSProjekat.Interfaces;
using System.Collections.Generic;

namespace PuSGSProjekat.Services
{
    public class ArticleService : IArticleService
    {
        private readonly DatabaseContext _dbContext;

        public ArticleService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ArticleResponseDTO CreateArticle(ArticleRequestDTO requestDto, long userId)
        {
            throw new System.NotImplementedException();
        }

        public List<ArticleResponseDTO> GetAllArticles(long SellerId)
        {
            throw new System.NotImplementedException();
        }

        public ArticleResponseDTO GetArticleById(long id)
        {
            throw new System.NotImplementedException();
        }

        public ArticleResponseDTO UpdateArticle(long id, ArticleRequestDTO requestDto, long userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
