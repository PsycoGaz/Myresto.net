using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using myresto.Data;
using myresto.DTO;


using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace myresto.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new()
                {
                    UserName = registerDTO.Email,
                    Email = registerDTO.Email,
                    Nom = registerDTO.Nom,        // Enregistrer le Nom
                    Prenom = registerDTO.Prenom   // Enregistrer le Prenom
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, registerDTO.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "Client");
                    return Ok("success");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(loginDTO.Email);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        var role = roles.FirstOrDefault();

                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role) // Ajout du rôle de l'utilisateur
                };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: creds
                        );

                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            username = loginDTO.Email,
                            role = role // Retourner également le rôle
                        };

                        return Ok(_token);
                    }
                    else
                    {
                        ModelState.AddModelError("Erreur", "Utilisateur n'est pas autorisé à se connecter");
                        return Unauthorized(ModelState);
                    }
                }
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDTOs = new List<NewUserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDTOs.Add(new NewUserDTO
                {
                    Email = user.Email,
                    Password = user.PasswordHash, // Note: It's not recommended to expose password hashes
                    Nom = user.Nom,  // Utilisation de Nom de l'utilisateur
                    Prenom = user.Prenom, // Utilisation de Prenom de l'utilisateur
                    Role = roles.FirstOrDefault() // Assuming a single role per user
                });
            }

            return Ok(userDTOs);
        }
    }
}