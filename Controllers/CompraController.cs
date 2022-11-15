using AutoMapper;
using DTOs;
using ecommerceApi.Context;
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

        public CompraController(RepositoryContext context, IMapper mapper, ILogger<CompraController> logger)
        {
            _services = new CompraServices(context, mapper);
            _logger = logger;    
        }

        [HttpPost]
        public async Task<IActionResult> AddCompra(ComprasDTO model)
        {
            ModelRequest res = new ModelRequest();
            if(model == null && !ModelState.IsValid)
            {
                _logger.LogError("El modelo de compra es invalido");
                res.status = "Error";
                res.data = "El modelo de compra es invalido";
                return BadRequest(res);
            }

            var result = await _services.CrearCompra(model);

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
            if(result == null)
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