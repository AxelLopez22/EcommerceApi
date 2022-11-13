using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class ProveedoresDTO
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string RazonSocial { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Ruc { get; set; }
    }

    public class AgregarProveedoresDTO
    {
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string RazonSocial { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string? Ruc { get; set; }
    }
}