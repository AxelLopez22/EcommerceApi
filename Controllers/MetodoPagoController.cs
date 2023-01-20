using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using ecommerceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ecommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoPagoController : ControllerBase
    {
        private readonly ILogger<MetodoPagoController> _logger;
        private readonly MetodoPagoServices _services;

        public MetodoPagoController(ILogger<MetodoPagoController> logger, ContextDb context, IMapper mapper)
        {
            _services = new MetodoPagoServices(context, mapper);
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetMetodosPagos()
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetMetodosPagos();
            if (result == null)
            {
                _logger.LogError("Ocurrio un error al mostrar metodos de pago");
                res.status = "Error";
                res.data = "Ocurrio un error, no hay metodos de pagos para mostrar";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }
    }
}
