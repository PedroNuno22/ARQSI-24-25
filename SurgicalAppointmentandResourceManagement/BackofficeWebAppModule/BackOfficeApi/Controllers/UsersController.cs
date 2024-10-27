using BackOfficeApi.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BackOfficeApi.Controllers
{
    [ApiController] //apenas os admins acedem a esta rota
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsersController(UserManager<IdentityUser> userManager, 
                               RoleManager<IdentityRole> roleManager,
                               SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost("register")] // Método para registrar novos usuários
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, "SenhaFort3!"); // Pode alterar para usar o model.Password se preferir

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(model.Role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(model.Role));
                    }

                    await _userManager.AddToRoleAsync(user, model.Role);

                    return Ok(new { Message = "Usuário registrado com sucesso!" });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Dados inválidos.");
        }

        //[AllowAnonymous] //permite todos
        [HttpPost("login")]// Método para login de usuários
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Login bem-sucedido" });
                }
                else if (result.IsLockedOut)
                {
                    return BadRequest("Conta bloqueada devido a várias tentativas de login inválidas.");
                }

                return Unauthorized("Credenciais inválidas.");
            }

            return BadRequest("Dados de login inválidos.");
        }
    }
}
