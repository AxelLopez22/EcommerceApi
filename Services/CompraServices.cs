using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CompraServices
    {
        //private readonly RepositoryContext _context;
        private readonly ContextDb _contextDb;
        private readonly IMapper _mapper;

        public CompraServices(IMapper mapper, ContextDb contextDb)
        {
            _mapper = mapper;
            _contextDb = contextDb;
        }   

        public async Task<ComprasDTO> CrearCompra(string IdUsuario, ComprasDTO model)
        {
            var transaction = await _contextDb.Database.BeginTransactionAsync();
            try
            {
                Compra compra = new Compra();
                compra.IdProveedor = model.IdProveedor;
                compra.UsuarioId = IdUsuario;
                compra.Estado = true;
                compra.FechaCompra = DateTime.Now;
                compra.IdMetodoPago = model.IdMetodoPago;
                compra.Total = model.Detalle.Sum(x => x.Precio * x.Cantidad);
                await _contextDb.Compras.AddAsync(compra);
                await _contextDb.SaveChangesAsync();

                foreach(var item in model.Detalle)
                {
                    var producto = await _contextDb.Productos.Where(x => x.IdProductos == item.IdProducto)
                        .FirstOrDefaultAsync();
                    producto.Stock = producto.Stock + item.Cantidad;
                    _contextDb.Productos.Update(producto);
                    await _contextDb.SaveChangesAsync();

                    DetalleCompra detalle = new DetalleCompra();
                    detalle.Cantidad = item.Cantidad;
                    detalle.Precio = item.Precio;
                    detalle.IdProducto = item.IdProducto;
                    detalle.IdCompra = compra.IdCompra;
                    await _contextDb.DetalleCompras.AddAsync(detalle);
                    await _contextDb.SaveChangesAsync();
                }
                await transaction.CommitAsync();
                return _mapper.Map<ComprasDTO>(model);
            }
            catch
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<bool> AnularCompra(int id)
        {
            try
            {
                var Compra = await _contextDb.Compras.Where(x => x.IdCompra == id && x.Estado == true)
                    .FirstOrDefaultAsync();

                if (Compra == null) { return false; }

                Compra.Estado = false;
                _contextDb.Compras.Update(Compra);
                await _contextDb.SaveChangesAsync();
                foreach (var item in Compra.DetalleCompras)
                {
                    var producto = await _contextDb.Productos
                        .Where(x => x.IdProductos == item.IdProductoNavigation.IdProductos).FirstOrDefaultAsync();
                    producto.Stock = producto.Stock - item.Cantidad;
                    _contextDb.Productos.Update(producto);
                    await _contextDb.SaveChangesAsync();
                }
                return true;
            } catch
            {
                return false;
            }
        }  
    }
}