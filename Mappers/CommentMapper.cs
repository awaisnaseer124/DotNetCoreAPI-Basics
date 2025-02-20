using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comments;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using api.Models;
namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentsDto ToCommentDto(this Comment commentModel)
        {
            return new CommentsDto
            {
                Id = commentModel.Id,
                StockId=commentModel.StockId,
                Title=commentModel.Title,
                Content=commentModel.Content,
                CreatedOn=commentModel.CreatedOn


            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentsDto commentsDto,int stockId)
        {
            return new Comment
            {
              
                StockId=stockId,
                Title=commentsDto.Title,
                Content=commentsDto.Content

            };
        }

          public static Comment ToCommentFromUpdate(this UpdateCommentDto commentsDto)
        {
            return new Comment
            {
              
                Title=commentsDto.Title,
                Content=commentsDto.Content

            };
        }
    }
}