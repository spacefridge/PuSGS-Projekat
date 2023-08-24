using Microsoft.EntityFrameworkCore;
using PuSGSProjekat.Models;
using System.Collections.Generic;

namespace PuSGSProjekat.Interfaces.Repositories
{
    public interface IArticleRepositories
    {

         void CreateArticle(Article article);
         List<Article> GetAllArticles(long sellserID);
         Article GetArticleById(long id);
         void DeleteArticle( Article article);
         void SaveChanges();
    }
}
