using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using ecommerceApi.DTOs;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ecommerceApi.Services
{
    public class CarritoServices
    {
        private readonly ContextDb _context;
        private readonly IMapper _mapper;

        public CarritoServices(ContextDb context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AgregarAlCarrito(string IdUsuario, int IdProducto)
        {
            try
            {
                Carrito cart = new Carrito();
                cart.IdUsuario = IdUsuario;
                cart.IdProducto = IdProducto;
                _context.Carritos.Add(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CarritoDTO> MostrarCarrito(string IdUsuario)
        {
            try
            {
                CarritoDTO cart = new CarritoDTO();
                List<Carrito> carrito = new List<Carrito>();
                carrito = await _context.Carritos.Where(x => x.IdUsuario == IdUsuario).ToListAsync();

                foreach (var id in carrito.Where(x => x.IdProducto == x.IdProducto))
                {
                    Producto producto = new Producto();
                    ProductoDTO product = new ProductoDTO();
                    producto = await _context.Productos.Where(x => x.IdProductos == id.IdProducto).FirstOrDefaultAsync();
                    product = _mapper.Map<ProductoDTO>(producto);
                    cart.Productos.Add(product);
                }
                return cart;

            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CarritoDetalleDTO>> MostrarCarritoDetalle(string idUsuario)
        {
            try
            {
                List<CarritoDetalleDTO> cartDetails = new List<CarritoDetalleDTO>();
                List<Carrito> carrito = new List<Carrito>();
                carrito = await _context.Carritos.Where(x => x.IdUsuario == idUsuario).ToListAsync();

                foreach(var id in carrito.Where(x => x.IdProducto == x.IdProducto))
                {
                    CarritoDetalleDTO cart = new CarritoDetalleDTO();
                    var carr = new List<Carrito>();
                    carr = await _context.Carritos.Where(x => x.IdProducto == id.IdProducto).ToListAsync();
                    var prod = await _context.Productos.Where(x => x.IdProductos == id.IdProducto).FirstOrDefaultAsync();
                    cart.cantidad = carr.Count;
                    cart.idProducto = id.IdProducto;
                    cart.precio = (double)prod.Precio;
                    cart.compra = true;
                    var existe = cartDetails.Where(x => x.idProducto == id.IdProducto).FirstOrDefault();
                    if (existe == null)
                    {
                        cartDetails.Add(cart);
                    }
                }

                return cartDetails;
            } catch 
            {
                return null;
            }
        }
    }
}
