using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaServices _services;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(RepositoryContext context, IMapper mapper, ILogger<CategoriaController> logger)
        {
            _services = new CategoriaServices(context, mapper);
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetCategorias();
            if(result == null)
            {
                _logger.LogError("No hay categorias para mostrar");
                res.status = "Error";
                res.data = "No hay categorias para mostrar";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoria(CreateCategoriaDTO model)
        {
            ModelRequest res = new ModelRequest();
            if(model == null || !ModelState.IsValid)
            {
                _logger.LogError("El modelo ingresado no es valido");
                res.status = "Error";
                res.data = "El objeto es invalido";
                return BadRequest(res);
            }
            var result = await _services.AddCategoria(model);
            if(result == null)
            {
                _logger.LogError("Ocurrio un error al agregar categoria");
                res.status = "Error";
                res.data = "Ocurrio un error al agregar categoria";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = "Agregado con exito";
            return Ok(res);
        }
    }
}