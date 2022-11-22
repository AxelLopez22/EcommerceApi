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
                    return ConstruirToken(model.NameUser);
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
                    return ConstruirToken(model.NameUser);
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

        private RespuestaAutenticacion ConstruirToken(string UserName)
        {
            var Claims = new List<Claim>()
            {
                new Claim("Usuario", UserName)
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["LlaveJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var Expiracion = DateTime.UtcNow.AddHours(1);
            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: Claims,
                expires: Expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = Expiracion
            };
        }
    }
}