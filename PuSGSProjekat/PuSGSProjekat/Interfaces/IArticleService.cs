using PuSGSProjekat.DTO.ArticleDTO;
using System.Collections.Generic;

namespace PuSGSProjekat.Interfaces
{
    public interface IArticleService
    {
        List<ArticleResponseDTO> GetAllArticles(long SellerId);
        ArticleResponseDTO GetArticleById(long id);
        ArticleResponseDTO CreateArticle(ArticleRequestDTO requestDto, long userId);
        ArticleResponseDTO UpdateArticle(long id, ArticleRequestDTO requestDto, long userId);
   
    }
}
