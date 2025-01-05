﻿using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Dtos.Comments;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Interfaces.Comments;

namespace TaskManagement.BuisnessLogic.Services
{
    public class CommentRepository(AppDbContext context) : ICommentRepository
    {
        private readonly AppDbContext context = context;

        public async Task<CommentResponse> CreateComment(int id, CreateComment comment, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FindAsync(id);
            var mapcomment = comment.Adapt<Comments>();
            mapcomment.TaskId = id;
            await context.Comments.AddAsync(mapcomment,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return mapcomment.Adapt<CommentResponse>();
        }
    }
}
