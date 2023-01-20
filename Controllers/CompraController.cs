using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompraController : ControllerBase
    {
        private readonly ILogger<CompraController> _logger;
        private readonly CompraServices _services;
        private readonly UserManager<IdentityUser> _userManager;

        public CompraController(IMapper mapper, ILogger<CompraController> logger,
        UserManager<IdentityUser> userManager, ContextDb contextDb)
        {
            _services = new CompraServices(mapper, contextDb);
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddCompra(ComprasDTO model)
        {
            var Userclaim = HttpContext.User.Claims.Where(claim => claim.Type == "Usuario").FirstOrDefault();
            var user = Userclaim.Value;
            var Usuario = await _userManager.FindByNameAsync(user);
            var IdUsuario = Usuario.Id;

            ModelRequest res = new ModelRequest();
            if(model == null && !ModelState.IsValid)
            {
                _logger.LogError("El modelo de compra es invalido");
                res.status = "Error";
                res.data = "El modelo de compra es invalido";
                return BadRequest(res);
            }
            var result = await _services.CrearCompra(IdUsuario ,model);
            if(result == null)
            {
                _logger.LogError("Ha ocurrido un error al procesar la venta");
                res.status = "Error";
                res.data = "Ha ocurrido un error al procesar la venta";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = "Compra exitosa";
            return Ok(res);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> AnularCompra(int id)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.AnularCompra(id);
            if(result == false)
            {
                _logger.LogError("Ocurrio un error al anular compra");
                res.status = "Error";
                res.data = "Ocurrio un error al anular la compra";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }
    }
}