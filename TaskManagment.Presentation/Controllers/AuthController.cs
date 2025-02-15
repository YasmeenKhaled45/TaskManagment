﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.BuisnessLogic.Contracts.Users.Queries;
using TaskManagement.BuisnessLogic.DataSantization;
using TaskManagement.DataAccess.Dtos.Auth;
using TaskManagement.DataAccess.Interfaces;
using LoginRequest = TaskManagement.DataAccess.Dtos.Auth.LoginRequest;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService repository;

        public AuthController(IUserService repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [ServiceFilter(typeof(InputSanitizationFilter))]
        public async Task<IActionResult> RegisterAsync(RegisterRequestdto request , CancellationToken cancellationToken)
        {
            var res = await repository.RegisterAsync(request, cancellationToken);
            return Ok(res);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(InputSanitizationFilter))]
        public async Task<IActionResult> LoginAsync(LoginRequest request , CancellationToken cancellationToken)
        {
            var res = await repository.LoginAsync(request,cancellationToken);
            return res.IsSuccess ? Ok(res.Value) : BadRequest(res.Error);
        }
    }
}
