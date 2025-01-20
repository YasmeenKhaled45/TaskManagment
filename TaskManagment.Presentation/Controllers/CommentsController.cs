﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.BuisnessLogic.DataSantization;
using TaskManagement.DataAccess.Dtos.Comments;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController(ICommentService repository) : ControllerBase
    {
        private readonly ICommentService repository = repository;

        [HttpPost("{id}")]
        [ServiceFilter(typeof(InputSanitizationFilter))]
        [Authorize]
        public async Task<IActionResult> AddComment([FromRoute]int id , CreateComment comment , CancellationToken cancellationToken)
        {
            var result = await repository.CreateComment(id, comment , cancellationToken);
            return Ok(result);
        }
    }
}
