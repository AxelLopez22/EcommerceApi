using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ProductoServices
    {
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _http;
        private readonly string contenedor = "Images";

        public ProductoServices(RepositoryContext context, IMapper mapper, IWebHostEnvironment env, IHttpContextAccessor http)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
            _http = http;
        }

        public async Task<CreateProductoDTO> AddProduct(CreateProductoDTO model)
        {
            try
            {
                Producto producto = new Producto();
                producto.NombreProducto = model.NombreProducto;
                producto.Descripcion = model.Descripcion;
                producto.IdCategoria = model.IdCategoria;
                producto.Precio = model.Precio;
                if(model.IdCategoria == 2)
                {
                    producto.Stock = null;
                }
                producto.Stock = 0;
                producto.Estado = true;

                if(model.Foto != null)
                {
                    using(var memoryStream = new MemoryStream())
                    {
                        await model.Foto.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(model.Foto.FileName);
                        producto.ImagenUrl = await GuardarArchivo(contenido, extension,contenedor,
                            model.Foto.ContentType);
                    }
                }

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return model;
            } catch
            {
                return null;
            }
        }

        public async Task<ProductoDTO> GetIdProduct(int id)
        {
            try
            {
                var result = await _context.Productos.Where(x => x.IdProductos == id && x.Estado == true)
                    .Select(s => new ProductoDTO(){
                        IdProductos = s.IdProductos,
                        NombreProducto = s.NombreProducto,
                        Descripcion = s.Descripcion,
                        Stock = s.Stock,
                        Precio = s.Precio,
                        ImagenUrl = s.ImagenUrl,
                        IdCategoria = s.IdCategoria,
                        NombreCategoria = s.IdCategoriaNavigation.Nombre
                    }).FirstOrDefaultAsync();
                if(result == null)
                {
                    return null;
                }
                return result;
            } catch 
            {
                return null;
            }
        }

        public async Task<List<ProductoDTO>> GetProductsCategories(int IdCategoria, int Cantidad)
        {
            try
            {
                List<ProductoDTO> producto = new List<ProductoDTO>();
                producto = await _context.Productos.Where(x => x.Estado == true && x.IdCategoria == IdCategoria)
                    .Select(s => new ProductoDTO()
                    {
                        IdProductos = s.IdProductos,
                        NombreProducto = s.NombreProducto,
                        Descripcion = s.Descripcion,
                        Stock = s.Stock,
                        Precio = s.Precio,
                        ImagenUrl = s.ImagenUrl,
                        IdCategoria = s.IdCategoria,
                        NombreCategoria = s.IdCategoriaNavigation.Nombre
                    }).Take(Cantidad).ToListAsync();

                if (producto.Count == 0)
                {
                    return null;
                }
                return producto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ProductoDTO>> GetProducts()
        {
            try
            {
                List<ProductoDTO> producto = new List<ProductoDTO>();
                producto = await _context.Productos.Where(x => x.Estado == true)
                    .Select(s => new ProductoDTO(){
                        IdProductos = s.IdProductos,
                        NombreProducto = s.NombreProducto,
                        Descripcion = s.Descripcion,
                        Stock = s.Stock,
                        Precio = s.Precio,
                        ImagenUrl = s.ImagenUrl,
                        IdCategoria = s.IdCategoria,
                        NombreCategoria = s.IdCategoriaNavigation.Nombre
                    }).ToListAsync();
                
                if(producto.Count == 0)
                {
                    return null;
                }
                return producto;
            } catch 
            {
                return null;
            }
        }

        public async Task<CreateProductoDTO> UpdateProducto(int id, CreateProductoDTO model)
        {
            try
            {
                var Producto = await _context.Productos.FirstOrDefaultAsync(x => x.IdProductos == id);
                if(Producto == null){return null;}

                Producto = _mapper.Map(model, Producto);

                if(model.Foto != null)
                {
                    using(var memoryStream = new MemoryStream())
                    {
                        await model.Foto.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(model.Foto.FileName);
                        Producto.ImagenUrl = await GuardarArchivo(contenido, extension,contenedor,
                            model.Foto.ContentType);
                    }
                }

                await _context.SaveChangesAsync(); 
                return model;
            } catch 
            { 
                return null;
            }
        }

        public async Task<bool> DeleteProducto(int id)
        {
            try
            {
                var existe = await _context.Productos.Where(x => x.IdProductos == id && x.Estado == true)
                    .FirstOrDefaultAsync();
                
                if(existe == null)
                {
                    return false;
                }
                existe.Estado = false;
                _context.Productos.Update(existe);
                await _context.SaveChangesAsync();
                return true;
            } catch 
            {
                return false;
            }
        }

//#########################################################################################//
        private async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";
            var folder = Path.Combine(_env.WebRootPath,contenedor);
            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, contenido);

            var UrlActual = $"{_http.HttpContext.Request.Scheme}://{_http.HttpContext.Request.Host}";
            var UrlParaDb = Path.Combine(UrlActual, contenedor, nombreArchivo).Replace("\\","/");
            return UrlParaDb;
        }

        private Task BorrarArchivo(string ruta, string contenedor)
        {
            if(ruta != null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorioArchivo = Path.Combine(_env.WebRootPath, contenedor, nombreArchivo);

                if(File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }
            }
            return Task.FromResult(0);
        }

        private async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor
            , string ruta, string contentType)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }
    }
}