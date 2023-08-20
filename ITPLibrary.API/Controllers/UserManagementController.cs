using ITPLibrary.Data.Shared.Dtos.UserDtos;

namespace ITPLibrary.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/usermanagement")]
[Produces("application/json")]
public class UserManagementController : Controller
{
    private readonly IUserService _userManagementService;

    public UserManagementController(IUserService userManagementService)
    {
        _userManagementService = userManagementService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> Login(UserDto user)
    {
        var token = await _userManagementService.Login(user);

        return token != null ? Ok(token) :
                               Unauthorized("Invalid credentials");
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> Register([FromBody]UserDto user)
    {
        var registered = await _userManagementService.Register(user);

        return registered ? Created(user.UserEmail, "Account created with success") :
                            Conflict("An account already exists with the provided email address");
    }

    [HttpGet("recoverPassword/{email}")]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> RecoverPassword([FromRoute] string email)
    {
        var success = await _userManagementService.RecoverPassword(email);

        return success ? Ok("Email sent with your password") :
                         NotFound("No user found with the provided email address");
    }
}
