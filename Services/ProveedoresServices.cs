using AutoMapper;
using DTOs;
using ecommerceApi.Common.Paginado;
using ecommerceApi.Context;
using ecommerceApi.DTOs;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Services
{
    public class ProveedoresServices
    {
        private readonly ContextDb _context;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;

        public ProveedoresServices(ContextDb context, IMapper mapper, HttpContext httpContext)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<List<ProveedoresDTO>> GetProveedores(PaginacionDTO paginacion)
        {
            try
            {
                var result = await _context.Proveedores.Where(x => x.Estado == true).OrderBy(x => x.Nombre)
                    .Paginar(paginacion).ToListAsync();
                if(result.Count == 0)
                {
                    return null;
                }
                return _mapper.Map<List<ProveedoresDTO>>(result);
            } 
            catch
            {
                return null;
            }
        }

        public async Task<ProveedoresDTO> GetIdProveedor(int id)
        {
            try
            {
                var result = await _context.Proveedores.Where(x => x.IdProveedor == id && x.Estado == true)
                    .FirstOrDefaultAsync();
                if(result == null)
                {
                    return null;
                }
                return _mapper.Map<ProveedoresDTO>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<AgregarProveedoresDTO> AddProveedor(AgregarProveedoresDTO model)
        {
            try
            {
                Proveedore proveedores = new Proveedore();
                proveedores = _mapper.Map<Proveedore>(model);
                proveedores.Estado = true;
                
                _context.Proveedores.Add(proveedores);
                await _context.SaveChangesAsync();

                return _mapper.Map<AgregarProveedoresDTO>(proveedores);
            }
            catch
            {
                return null;
            }
        }

        public async Task<AgregarProveedoresDTO> UpdateProveedor(int id,AgregarProveedoresDTO model)
        {
            try
            {
                var existe = await _context.Proveedores.Where(x => x.IdProveedor == id && x.Estado == true)
                    .FirstOrDefaultAsync();
                if(existe == null)
                {
                    return null;
                }
                existe.Nombre = model.Nombre;
                existe.RazonSocial = model.RazonSocial;
                existe.Email = model.Email;
                existe.Ruc = model.Ruc;

                _context.Proveedores.Update(existe);
                await _context.SaveChangesAsync();

                return _mapper.Map<AgregarProveedoresDTO>(existe);
            } 
            catch 
            {
                return null;
            }
        }

        public async Task<bool> DeleteProveedor(int id)
        {
            try
            {
                var existe = await _context.Proveedores.Where(x => x.IdProveedor == id && x.Estado == true)
                    .FirstOrDefaultAsync();
                
                if(existe == null)
                {
                    return false;
                }
                existe.Estado = false;
                _context.Proveedores.Update(existe);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}