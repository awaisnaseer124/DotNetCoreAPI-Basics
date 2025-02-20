using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comments;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsycn();
        public Task<Comment?> GetCommentByIdAsycn(int id);
        public Task<Comment> CreateCommentAsync(Comment commentModel);
        public Task<Comment?> UpdateCommentAsync(int id,Comment commentMOdel);
        public Task<Comment?> DeleteCommentAsync(int id);
    }
}