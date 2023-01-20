using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using ecommerceApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ecommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly ILogger<CarritoController> _logger;
        private readonly CarritoServices _carritoServices;
        private readonly UserManager<IdentityUser> _userManager;

        public CarritoController(ILogger<CarritoController> logger, ContextDb context, UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            _logger = logger;
            _carritoServices = new CarritoServices(context, mapper);
            _userManager = userManager;
        }

        [HttpPost("{IdProducto:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AgregarAlCarrito(int IdProducto)
        {
            var Userclaim = HttpContext.User.Claims.Where(claim => claim.Type == "Usuario").FirstOrDefault();
            var user = Userclaim.Value;
            var Usuario = await _userManager.FindByNameAsync(user);
            var IdUsuario = Usuario.Id;

            ModelRequest res = new ModelRequest();
            var result = await _carritoServices.AgregarAlCarrito(IdUsuario, IdProducto);

            if (result == false)
            {
                _logger.LogError("Ocurrio un error al agregar al carrito");
                res.status = "Error";
                res.data = "Ocurrio un error al agregar el producto al carrito";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = "Agregado con exito";
            return Ok(res);
        }

        [HttpGet()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ObtenerCarrito()
        {
            var UserClaim = HttpContext.User.Claims.Where(claim => claim.Type == "Usuario").FirstOrDefault();
            var user = UserClaim.Value;
            var Usuario = await _userManager.FindByNameAsync(user);
            var IdUsuario = Usuario.Id;

            ModelRequest res = new ModelRequest();
            var result = await _carritoServices.MostrarCarrito(IdUsuario);
            if (result == null)
            {
                _logger.LogError("Error al mostrar carrito");
                res.status = "Error";
                res.data = "Ocurrio un error al mostrar productos del carrito";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }

        [HttpGet("carritoDetalle")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> MostrarCarritoDetalle()
        {
            var UserClaim = HttpContext.User.Claims.Where(claim => claim.Type == "Usuario").FirstOrDefault();
            var user = UserClaim.Value;
            var Usuario = await _userManager.FindByNameAsync(user);
            var IdUsuario = Usuario.Id;

            ModelRequest res = new ModelRequest();
            var result = await _carritoServices.MostrarCarritoDetalle(IdUsuario);
            if (result == null)
            {
                _logger.LogError("Error al mostrar carrito");
                res.status = "Error";
                res.data = "Ocurrio un error al mostrar productos del carrito";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }
    }
}
