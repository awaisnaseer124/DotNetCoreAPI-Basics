using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comments;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;

namespace api.Controllers
{
    [Route("api/Comment")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepo = commentRepository;
            _stockRepo = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var comments = await _commentRepo.GetAllAsycn();
            var commentDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var comment = await _commentRepo.GetCommentByIdAsycn(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);

        }

        [HttpPost]
        [Route("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentsDto createCommentsDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool stockExist = await _stockRepo.StockExist(stockId);
            if (!stockExist)
            {
                return BadRequest("stock doesn't exist");
            }

            var commentModel = createCommentsDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto commentDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var commentModel = await _commentRepo.UpdateCommentAsync(id, commentDto.ToCommentFromUpdate());
            if (commentModel == null)
            {
                return BadRequest("comment doesn't exist");
            }

            return Ok(commentModel.ToCommentDto());

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var commentModel = await _commentRepo.DeleteCommentAsync(id);
            if (commentModel == null)
            {
                return NotFound("Comment does not exist");
            }
            return Ok(commentModel.ToCommentDto());
        }
    }
}