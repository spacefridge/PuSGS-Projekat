using PuSGSProjekat.Context;
using PuSGSProjekat.Interfaces.Repositories;
using PuSGSProjekat.Models;
using System.Collections.Generic;
using System.Linq;

namespace PuSGSProjekat.Repositories
{
    public class ArticleRepository : IArticleRepositories
    {

        private readonly DatabaseContext _dbContext;

        public ArticleRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateArticle(Article article)
        {
            _dbContext.Articles.Add(article);
        }

        public void DeleteArticle(Article article)
        {
             _dbContext.Articles.Remove(article);
        }

        public List<Article> GetAllArticles(long sellserID)
        {

            List<Article> articles = new List<Article>();

            if (sellserID > 0)
            {
                articles = _dbContext.Articles.Where(x => x.SellerId == sellserID).ToList();
            }
            else
            {
                articles = _dbContext.Articles.ToList();
            }


            return articles;
        }

        public Article GetArticleById(long id)
        {         
            return _dbContext.Articles.Find(id);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
