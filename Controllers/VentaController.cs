using AutoMapper;
using Common.Extensions;
using DTOs;
using ecommerceApi.Common.Paginado;
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
    public class VentaController : ControllerBase
    {
        private readonly ILogger<VentaController> _logger;
        private readonly VentaServices _services;
        private readonly UserManager<IdentityUser> _userManager;

        public VentaController(IMapper mapper, ILogger<VentaController> logger,
        UserManager<IdentityUser> userManager, ContextDb contextDb)
        {
            _services = new VentaServices(mapper, contextDb);
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CrearVenta(VentasDTO model)
        {
            var Userclaim = HttpContext.User.Claims.Where(claim => claim.Type == "Usuario").FirstOrDefault();
            var user = Userclaim.Value;
            var Usuario = await _userManager.FindByNameAsync(user);
            var IdUsuario = Usuario.Id;

            ModelRequest res = new ModelRequest();
            var result = await _services.CrearVenta(IdUsuario, model);
            if(result == null)
            {
                _logger.LogError("Ocurrio un error al procesar la compra");
                res.status = "Error";
                res.data = "Ocurrio un error al hacer la venta";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = "Compra hecha con exito";
            return Ok(res);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> AnularVenta(int id)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.AnularVenta(id);
            if(result == false)
            {
                _logger.LogError("Ocurrio un error al anular la venta");
                res.status = "Error";
                res.data = "Ocurrio un error al anular la venta";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = "Venta anulada con exito";
            return Ok(res);
        }
    }
}