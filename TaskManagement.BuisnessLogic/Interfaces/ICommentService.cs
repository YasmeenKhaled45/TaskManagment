using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Comments.Commands;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos.Comments;

namespace TaskManagement.DataAccess.Interfaces
{
    public interface ICommentService
    {
        Task<Result<CommentResponse>> CreateComment(int id, string Content, CancellationToken cancellationToken);
    }
}
