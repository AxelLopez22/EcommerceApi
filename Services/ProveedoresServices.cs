using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ProveedoresServices
    {
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;

        public ProveedoresServices(RepositoryContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProveedoresDTO>> GetProveedores()
        {
            try
            {
                var result = await _context.Proveedores.Where(x => x.Estado == true).ToListAsync();
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