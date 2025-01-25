using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Users.Commands;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Dtos.Auth;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Errors;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagement.BuisnessLogic.Services
{
    public class UserService(UserManager<User> userManager, IOptions<JWT> jwt) : IUserService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly JWT jWT = jwt.Value;

        public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Failure<AuthResponse>(AuthErrors.InvalidCredentials);
            if (user.AccessFailedCount >= 5)
            {
                return Result.Failure<AuthResponse>(AuthErrors.LockedUser);
            }
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (result)
            {
                var jwtToken = await CreateJwtToken(user);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

               
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(jWT.ExpiryMinutes);

                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpiration
                });
                await _userManager.UpdateAsync(user);

                var response = new AuthResponse(
                             user.Id,
                    user.FirstName,
                   user.LastName,
                  user.Email,
                 tokenString,
                 (int)TimeSpan.FromMinutes(jWT.ExpiryMinutes).TotalSeconds,
                refreshToken,
                 refreshTokenExpiration);
                         
                return Result.Success(response);

            }
            return Result.Failure<AuthResponse>(AuthErrors.InvalidCredentials);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWT.key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jWT.Issuer,
                audience: jWT.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jWT.ExpiryMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        public async Task<Result> RegisterAsync(RegisterRequestdto request, CancellationToken cancellationToken)
        {
            var emailexists =  await _userManager.FindByEmailAsync(request.Email);
            if(emailexists is not null) 
                return Result.Failure(AuthErrors.DuplicatedEmail);

            var user = request.Adapt<User>();
            user.UserName = request.Email;
            if (string.IsNullOrEmpty(user.UserName))
            {
                return Result.Failure(AuthErrors.UserName);
            }
            var res = await _userManager.CreateAsync(user,request.Password);
            if (!res.Succeeded)
            {
                var errors = string.Join(", ", res.Errors.Select(e => e.Description));
                return Result.Failure(new Error("Registration Failed!", errors));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded)
            {
                var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return Result.Failure(new Error("Role Assignment Failed!", roleErrors));
            }

            return Result.Success();
        }

        public async Task<Result<UserProfileResponse>> GetUserProfile(string UserId)
        {
            var user = await _userManager.Users.Where(x => x.Id == UserId).
                ProjectToType<UserProfileResponse>().SingleOrDefaultAsync();
            return Result.Success(user!);

        }

        public async Task<Result> UpdateProfileAsync(UpdateProfileCommand profileCommand, CancellationToken cancellationToken)
        {
            await _userManager.Users.Where(x => x.Id == profileCommand.UserId).
                ExecuteUpdateAsync(setter =>
                setter.SetProperty(x => x.FirstName, profileCommand.FirstName)
                .SetProperty(x => x.LastName, profileCommand.LastName));
            return Result.Success();
        }
    }
}
