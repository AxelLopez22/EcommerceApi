using AutoMapper;
using DTOs;
using ecommerceApi.Context;
using ecommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CategoriaServices
    {
        private Categorium _categoria;
        private readonly ContextDb _context;
        private readonly IMapper _mapper;

        public CategoriaServices(ContextDb context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoriaDTO>?> GetCategorias()
        {
            try
            {
                var result = await _context.Categoria.Where(x => x.Estado == true).ToListAsync();
                if(result.Count == 0)
                {
                    return null;
                }
                return _mapper.Map<List<CategoriaDTO>>(result);
            } catch 
            {
                return null;
            }
        }

        public async Task<CreateCategoriaDTO> AddCategoria(CreateCategoriaDTO model)
        {
            try 
            {
                _categoria = _mapper.Map<Categorium>(model);
                _categoria.Estado = true;
                await _context.Categoria.AddAsync(_categoria);
                await _context.SaveChangesAsync();
                return _mapper.Map<CreateCategoriaDTO>(_categoria);
            }
            catch
            {
                return null;
            }
        }
    }
}