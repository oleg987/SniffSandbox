using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SniffSandbox.Controllers;

public record SignUpModel(string Email, string Password);

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([Required] SignUpModel request, CancellationToken cancellationToken)
    {
        var user = new IdentityUser()
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        var roleName = "regular_user";

        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = roleName });
        }

        await _userManager.AddToRoleAsync(user, roleName);

        var claimPrincipal = await _signInManager.ClaimsFactory.CreateAsync(user);

        if (result.Succeeded)
        {
            return Ok(claimPrincipal.Claims);
        }

        return BadRequest(result.Errors);
    }
}