using AutoMapper;
using EntityFramework.Exceptions.Common;
using PuSGSProjekat.Context;
using PuSGSProjekat.DTO.ArticleDTO;
using PuSGSProjekat.DTO.DeleteDTO;
using PuSGSProjekat.ExceptionHandler;
using PuSGSProjekat.Interfaces;
using PuSGSProjekat.Interfaces.Repositories;
using PuSGSProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PuSGSProjekat.Services
{
    public class ArticleService : IArticleService
    {
       
        private readonly IMapper _mapper;
        private readonly IArticleRepositories _articleRepositories;

        public ArticleService(IArticleRepositories articleRepositories, IMapper mapper)
        {
            _articleRepositories = articleRepositories;
            _mapper = mapper;
        }

        public ArticleResponseDTO CreateArticle(ArticleRequestDTO requestDto, long userId)

        {
            Article article = _mapper.Map<Article>(requestDto);
            article.SellerId = userId;

            _articleRepositories.CreateArticle(article);

            try
            {
                _articleRepositories.SaveChanges();
            }
            catch (CannotInsertNullException)
            {
                throw new InvalidField("One of more fields are missing!");
            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.Map<ArticleResponseDTO>(article);
        }

        public DeleteResponseDTO DeleteArticle(long id, long userId)
        {
            Article article = _articleRepositories.GetArticleById(id);

            if (article == null)
            {
                throw new ResourceMissing("No article with given id.");
            }

            if (article.SellerId != userId)
            {
                throw new Forbidden("Not your article, cant proceed.");
            }

            _articleRepositories.DeleteArticle(article);
            _articleRepositories.SaveChanges();

            return _mapper.Map<DeleteResponseDTO>(article);
        }

        public List<ArticleResponseDTO> GetAllArticles(long SellerId)
        {
            List<Article> articles = new List<Article>();

            articles = _articleRepositories.GetAllArticles(SellerId);

            return _mapper.Map<List<ArticleResponseDTO>>(articles);
        }

        public ArticleResponseDTO GetArticleById(long id)
        {
            ArticleResponseDTO article = _mapper.Map<ArticleResponseDTO>(_articleRepositories.GetArticleById(id));

            if (article == null)
            {
                throw new ResourceMissing("No article with given id.");
            }

            return article;
        }

        public ArticleResponseDTO UpdateArticle(long id, ArticleRequestDTO requestDto, long userId)
        {
            Article article = _articleRepositories.GetArticleById(id);

            if (article == null)
            {
                throw new ResourceMissing("No article with given id.");
            }

            if (article.SellerId != userId)
            {
                throw new Forbidden("Not your article, cant proceed.");
            }

            _mapper.Map(requestDto, article);

            try
            {
                _articleRepositories.SaveChanges();
            }
            catch (CannotInsertNullException)
            {
                throw new InvalidField("Missing field/s");
            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.Map<ArticleResponseDTO>(article);
        }

        
    }
}
