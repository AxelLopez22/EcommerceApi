using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DTOs;
using DTOs.AuthModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Services
{
    public class UsuariosServices
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<IdentityUser> _signIngManager;
        public UsuariosServices(UserManager<IdentityUser> userManager, IConfiguration config, 
        SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager; 
            _config = config;
            _signIngManager = signInManager;
        }

        public async Task<RespuestaAutenticacion> Registrar(UsuariosDTO model)
        {
            try
            {
                var usuario = new IdentityUser
                {
                    UserName = model.NameUser,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                var resultado = await _userManager.CreateAsync(usuario, model.Password);

                if(resultado.Succeeded)
                {
                    return await ConstruirToken(model.NameUser);
                } else {
                    return (RespuestaAutenticacion)resultado.Errors;
                }
            } 
            catch
            {
                return null;
            }
        }

        public async Task<RespuestaAutenticacion> Login(LoginDTO model)
        {
            try
            {
                var resultado = await _signIngManager
                    .PasswordSignInAsync(model.NameUser,model.Password,isPersistent:false,lockoutOnFailure:false);
                
                if(resultado.Succeeded)
                {
                    return await ConstruirToken(model.NameUser);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private async Task<RespuestaAutenticacion> ConstruirToken(string UserName)
        {
            var Claims = new List<Claim>()
            {
                new Claim("Usuario", UserName)
            };

            var usuario = await _userManager.FindByNameAsync(UserName);
            var claimsDB = await _userManager.GetClaimsAsync(usuario);

            Claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["LlaveJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var Expiracion = DateTime.UtcNow.AddHours(1);
            var securityToken = new JwtSecurityToken(issuer: "appadminecommerce.azurewebsites.net", audience: "appadminecommerce.azurewebsites.net", claims: Claims,
                expires: Expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = Expiracion
            };
        }

        public async Task<bool> HacerAdmin(HacerAdminDTO model)
        {
            try
            {
                var usuario = await _userManager.FindByNameAsync(model.UserName);
                await _userManager.AddClaimAsync(usuario, new Claim ("Admin", "1"));
                return true;
            }   
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoverAdmin(HacerAdminDTO model)
        {
            try
            {
                var usuario = await _userManager.FindByNameAsync(model.UserName);
                await _userManager.RemoveClaimAsync(usuario, new Claim("Admin", "1"));
                return true;
            }
            catch
            {
                return false;
            }
        }
             
    }
}