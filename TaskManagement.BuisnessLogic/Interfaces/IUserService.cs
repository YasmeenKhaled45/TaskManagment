using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Dtos.Auth;

namespace TaskManagement.DataAccess.Interfaces
{
    public interface IUserService
    {
        Task<Result> RegisterAsync(RegisterRequestdto request, CancellationToken cancellationToken);
        Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
    }
}
