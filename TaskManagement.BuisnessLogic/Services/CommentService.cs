using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Dtos.Comments;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagement.BuisnessLogic.Services
{
    public class CommentService(AppDbContext context) : ICommentService
    {
        private readonly AppDbContext context = context;

        public async Task<CommentResponse> CreateComment(int id, CreateComment comment, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FindAsync(id);
            var mapcomment = comment.Adapt<Comments>();
            mapcomment.TaskId = id;
             context.Comments.Add(mapcomment);
            await context.SaveChangesAsync(cancellationToken);
            return mapcomment.Adapt<CommentResponse>();
        }
    }
}
