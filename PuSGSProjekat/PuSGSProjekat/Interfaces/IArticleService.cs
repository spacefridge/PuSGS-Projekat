using PuSGSProjekat.DTO.ArticleDTO;
using PuSGSProjekat.DTO.DeleteDTO;
using System.Collections.Generic;

namespace PuSGSProjekat.Interfaces
{
    public interface IArticleService
    {
        List<ArticleResponseDTO> GetAllArticles(long SellerId);
        ArticleResponseDTO GetArticleById(long id);
        ArticleResponseDTO CreateArticle(ArticleRequestDTO requestDto, long userId);
        ArticleResponseDTO UpdateArticle(long id, ArticleRequestDTO requestDto, long userId);
        DeleteResponseDTO DeleteArticle(long id, long userId);

    }
}
