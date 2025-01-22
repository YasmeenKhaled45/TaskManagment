using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos.Comments;

namespace TaskManagement.BuisnessLogic.Contracts.Comments.Commands
{
    public class CreateCommentCommand : IRequest<Result<CommentResponse>>
    {
        public int TaskId { get; set; }
        public string Content { get; set; }
    }
}
