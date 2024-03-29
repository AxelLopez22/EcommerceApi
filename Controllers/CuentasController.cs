using DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly ILogger<CuentasController> _logger;
        private readonly UsuariosServices _servicesUser;

        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration config, 
        SignInManager<IdentityUser> signInManager, ILogger<CuentasController> logger)
        {
            _servicesUser = new UsuariosServices(userManager,config ,signInManager);
            _logger = logger;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> AgregarUsuario(UsuariosDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _servicesUser.Registrar(model);
            if(result == null)
            {
                _logger.LogError("Ocurrio un error al registrar al usuario");
                res.status = "Error";
                res.data = "Ocurrio un error al registrar usuario";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _servicesUser.Login(model);
            if(result == null)
            {
                _logger.LogError("Error al iniciar sesion");
                res.status = "Error";
                res.data = "Ocurrio un error al iniciar sesión";
                return BadRequest(result);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }

        [HttpPost("HacerAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> HacerAdmin(HacerAdminDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _servicesUser.HacerAdmin(model);
            if(result == false)
            {
                res.status = "Error";
                res.data = "Ocurrio un error al crear Admin";
                return BadRequest(result);
            }
            res.status = "Ok";
            res.data = "Admin creado";
            return Ok(res);
        }
    }
}