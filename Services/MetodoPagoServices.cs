using AutoMapper;
using ecommerceApi.Context;
using ecommerceApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.Services
{
    public class MetodoPagoServices
    {
        private readonly ContextDb _context;
        private readonly IMapper _mapper;

        public MetodoPagoServices(ContextDb context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MetodosPagosDTO>> GetMetodosPagos()
        {
            var result = await _context.MetodoDePagos.Where(x => x.Estado == true).ToListAsync();
            if (result.Count == 0) { return null; }

            return _mapper.Map<List<MetodosPagosDTO>>(result);
        }
    }
}
