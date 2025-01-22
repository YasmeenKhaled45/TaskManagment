using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Comments.Commands;
using TaskManagement.BuisnessLogic.Services;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos.Comments;
using TaskManagement.DataAccess.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManagement.BuisnessLogic.Contracts.Comments.Handlers
{
    public class CommentCommandHandlers(ICommentService commentService,ITaskService taskService) : IRequestHandler<CreateCommentCommand, Result<CommentResponse>>
    {
        private readonly ICommentService commentService = commentService;
        private readonly ITaskService taskService = taskService;

        public async Task<Result<CommentResponse>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            return await commentService.CreateComment(request.TaskId, request.Content, cancellationToken);
        }

    }
}
