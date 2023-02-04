using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using ecommerceApi.DTOs;
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

        public ProductoController(ContextDb context, IMapper mapper, IWebHostEnvironment env, IHttpContextAccessor http,
            ILogger<ProductoController> logger)
        {
            _services = new ProductoServices(context, mapper, env, http);
            _logger = logger;
        }


        [HttpGet("viewProductos")]
        public async Task<IActionResult> ViewProductos()
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.ViewProductos();
            if(result == null)
            {
                _logger.LogError("Ocurrio un error a listar productos");
                res.status = "Error";
                res.data = "Ocurrio un error al mostrar productos";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetProducts([FromQuery] PaginacionDTO paginacion)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetProducts(paginacion);
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

        [HttpGet("obtenerProductos")]
        public async Task<IActionResult> GetProductCategories(int idCategoria, int Cantidad)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetProductsCategories(idCategoria, Cantidad);
            if(result == null) 
            {
                _logger.LogError($"Ocurrio un error al mostrar los productos");
                res.status = "Error";
                res.data = "No hay productos de esta categoria";
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
        public async Task<IActionResult> AddProduct([FromForm] CreateProductoDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.AddProduct(model);
            if(result == null)
            {
                _logger.LogError("Ocurrio un error al agregar producto");
                res.status = "Error";
                res.data = "Ocurrio un error al agregar el producto";
                return BadRequest(res);
            }
            res.status = "Ok";
            res.data = "Producto agregado exitosamente";
            return Ok(res);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id,[FromForm] CreateProductoDTO model)
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