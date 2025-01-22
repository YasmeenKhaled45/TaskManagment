using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Comments.Commands;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Dtos.Comments;
using TaskManagement.DataAccess.Dtos.Tasks;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Interfaces;
using TaskManagement.DataAccess.Migrations;

namespace TaskManagement.BuisnessLogic.Services
{
    public class CommentService(AppDbContext context) : ICommentService
    {
        private readonly AppDbContext context = context;

        public async Task<Result<CommentResponse>> CreateComment(int id, string Content, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task == null)
                return Result.Failure<CommentResponse>(new Error("Creation failed!", "Task not found with this Id"));
            var comment = new Comments
            {
                TaskId = id,
                Content = Content
            };

             context.Comments.Add(comment);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success(comment.Adapt<CommentResponse>());
        }
    }
}
