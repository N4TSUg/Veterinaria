using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Veterinaria.ViewModels.Consulta
{
    public class ConsultaCreateVM
    {
        [Required]
        public int IdMascota { get; set; }

        [Required(ErrorMessage = "La fecha de la consulta es obligatoria")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El motivo es obligatorio")]
        [StringLength(200)]
        public string Motivo { get; set; } = null!;

        [Required(ErrorMessage = "Los síntomas son obligatorios")]
        [StringLength(500)]
        public string Sintomas { get; set; } = null!;

        [Required(ErrorMessage = "El diagnóstico es obligatorio")]
        [StringLength(500)]
        public string Diagnostico { get; set; } = null!;

        [Required(ErrorMessage = "El tratamiento es obligatorio")]
        [StringLength(500)]
        public string Tratamiento { get; set; } = null!;

        public DateTime? FechaProximaCita { get; set; }
        
        public List<IFormFile>? Adjuntos { get; set; }
    }
}
