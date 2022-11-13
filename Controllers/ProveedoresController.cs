using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly ILogger<ProveedoresController> _logger;
        private readonly ProveedoresServices _services;

        public ProveedoresController(ILogger<ProveedoresController> logger, IMapper mapper, RepositoryContext context)
        {
            _logger = logger;
            _services = new ProveedoresServices(context, mapper);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetIdAsync(int id)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetIdProveedor(id);
            if(result == null)
            {
                _logger.LogError("El proveedor no fue encontrado");
                res.status = "Error";
                res.data = $"El proveedor con el Id: {id} no existe";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetProveedores();
            if(result == null)
            {
                _logger.LogError("La lista esta vacia");
                res.status = "Error";
                res.data = "La lista está vacia";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddProveedor(AgregarProveedoresDTO model)
        {
            ModelRequest res = new ModelRequest();
            if(model == null || !ModelState.IsValid)
            {
                _logger.LogError("El modelo es invalido");
                res.status = "Error";
                res.data = "El model ingresado es invalido";
                return BadRequest(res);
            }
            var result = await _services.AddProveedor(model);
            if(result == null)
            {
                _logger.LogError("Ocurrio un error al agregar proveedor");
                res.status = "Error";
                res.data = "Ocurrio un error al agregar proveedor";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = "Producto agregado con exito";
            return Ok(res);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProveedor(int id, AgregarProveedoresDTO model)
        {
            ModelRequest res = new ModelRequest();
            if(model == null || !ModelState.IsValid)
            {
                _logger.LogError("El modelo es invalido");
                res.status = "Error";
                res.data = "El model ingresado es invalido";
                return BadRequest(res);
            }
            var result = await _services.UpdateProveedor(id, model);
            if(result == null)
            {
                _logger.LogError("Ocurrio un error al agregar proveedor");
                res.status = "Error";
                res.data = "Ocurrio un error al agregar proveedor";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = "El producto actualizado exitosamente";
            return Ok(res);
        }

    }
}