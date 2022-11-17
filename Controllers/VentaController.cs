using AutoMapper;
using DTOs;
using ecommerceApi.Context;
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

        public VentaController(RepositoryContext context, IMapper mapper, ILogger<VentaController> logger)
        {
            _services = new VentaServices(context, mapper);
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CrearVenta(VentasDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.CrearVenta(model);
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