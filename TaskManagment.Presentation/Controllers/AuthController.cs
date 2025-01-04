using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DataAccess.Dtos.Auth;
using TaskManagement.DataAccess.Interfaces.User;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository repository;

        public AuthController(IUserRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterRequestdto request , CancellationToken cancellationToken)
        {
            var res = await repository.RegisterAsync(request, cancellationToken);
            return Ok(res);
        }
    }
}
