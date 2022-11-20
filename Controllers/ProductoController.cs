using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoServices _services;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(RepositoryContext context, IMapper mapper, IWebHostEnvironment env, IHttpContextAccessor http,
            ILogger<ProductoController> logger)
        {
            _services = new ProductoServices(context, mapper, env, http);
            _logger = logger;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetProducts()
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetProducts();
            if(result == null)
            {
                _logger.LogError("No hay productos para mostrar");
                res.status = "Error";
                res.data = "No hay productos para mostrar";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetIdProducto(int id)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetIdProduct(id);
            if(result == null)
            {
                _logger.LogError("El producto no existe");
                res.status = "Error";
                res.data = $"El producto con el Id: {id} no existe";
                return NotFound(res);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromQuery] CreateProductoDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.AddProduct(model);
            if(result == null)
            {
                _logger.LogError("Ocurrio un error al agregar producto");
                res.status = "Error";
                res.data = "Ocurrio un error al agregar el producto";
                return BadRequest();
            }
            res.status = "Ok";
            res.data = "Producto agregado exitosamente";
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, CreateProductoDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.UpdateProducto(id, model);
            if(result == null)
            {
                _logger.LogError("Ocurrio un error al actualizar el producto");
                res.status = "Error";
                res.data = "Ocurrio un error al actualizar el producto";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = "Producto editado exitosamente";
            return Ok(res);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.DeleteProducto(id);
            if(result == false)
            {
                _logger.LogError("Ocurrio un error al eliminar producto");
                res.status = "Error";
                res.data = "Ocurrio un error al eliminar producto";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = "Producto eliminado exitosamente";
            return Ok(res);
        }
    }
}