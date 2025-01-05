﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Dtos.Comments;

namespace TaskManagement.DataAccess.Interfaces.Comments
{
    public interface ICommentRepository
    {
        Task<CommentResponse> CreateComment( int id ,CreateComment comment , CancellationToken cancellationToken);
    }
}
