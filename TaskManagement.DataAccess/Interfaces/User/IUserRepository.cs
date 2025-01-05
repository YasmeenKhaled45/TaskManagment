using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos.Auth;
using TaskManagement.DataAccess.Responses.User;

namespace TaskManagement.DataAccess.Interfaces.User
{
    public interface IUserRepository
    {
        Task<Result> RegisterAsync(RegisterRequestdto request , CancellationToken cancellationToken);
        Task<Result<AuthResponse>> LoginAsync(LoginRequest request , CancellationToken cancellationToken);
    }
}
