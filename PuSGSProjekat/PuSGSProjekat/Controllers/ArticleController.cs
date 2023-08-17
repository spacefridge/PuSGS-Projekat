using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuSGSProjekat.DTO.ArticleDTO;
using PuSGSProjekat.Interfaces;
using System;
using System.Linq;

namespace PuSGSProjekat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public IActionResult GetAllArticles([FromQuery] long SellerId)
        {
            return Ok(_articleService.GetAllArticles(SellerId));
        }

        [HttpGet("{id}")]
        public IActionResult GetArticleById(long id)
        {
            ArticleResponseDTO article;

            try
            {
                article = _articleService.GetArticleById(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(article);
        }

        [HttpPost]
        [Authorize(Roles = "Seller", Policy = "IsVerifiedSeller")]
        public IActionResult CreateArticle([FromBody] ArticleRequestDTO requestDto)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            ArticleResponseDTO article;

            try
            {
                article = _articleService.CreateArticle(requestDto, userId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(article);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Seller", Policy = "IsVerifiedSeller")]
        public IActionResult UpdateArticle(long id, [FromBody] ArticleRequestDTO requestDto)
        {
            long userId = long.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            ArticleResponseDTO article;

            try
            {
                article = _articleService.UpdateArticle(id, requestDto, userId);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(article);
        }
    }
}
