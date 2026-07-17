using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veterinaria.Models
{
    public class Mascota
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMascota { get; set; }

        [Required(ErrorMessage = "El cliente (dueño) es obligatorio")]
        public int IdCliente { get; set; }

        [ForeignKey(nameof(IdCliente))]
        public Cliente Cliente { get; set; } = null!;

        [Required(ErrorMessage = "El nombre de la mascota es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La especie es obligatoria (Ej: Perro, Gato)")]
        [StringLength(30)]
        public string Especie { get; set; } = null!;

        [Required(ErrorMessage = "La raza es obligatoria")]
        [StringLength(50)]
        public string Raza { get; set; } = null!;

        [Required(ErrorMessage = "El color es obligatorio")]
        [StringLength(30)]
        public string Color { get; set; } = null!;

        [Required(ErrorMessage = "El sexo es obligatorio")]
        [StringLength(10)]
        public string Sexo { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        public DateTime FechaIngreso { get; set; } = DateTime.UtcNow.AddHours(-5);

        [NotMapped]
        public string EdadCalculada
        {
            get
            {
                var hoy = DateTime.UtcNow.AddHours(-5);
                if (FechaNacimiento > hoy) return "Recién nacido";

                var mesesTotal = (hoy.Year - FechaNacimiento.Year) * 12 + hoy.Month - FechaNacimiento.Month;
                int dias = hoy.Day - FechaNacimiento.Day;
                if (dias < 0)
                {
                    mesesTotal--;
                    var mesAnterior = hoy.AddMonths(-1);
                    dias += DateTime.DaysInMonth(mesAnterior.Year, mesAnterior.Month);
                }

                int anios = mesesTotal / 12;
                int mesesRestantes = mesesTotal % 12;

                if (anios >= 1)
                {
                    return $"{anios} año(s)" + (mesesRestantes > 0 ? $" y {mesesRestantes} mes(es)" : "");
                }
                else
                {
                    return $"{mesesRestantes} mes(es)" + (dias > 0 ? $" y {dias} día(s)" : "");
                }
            }
        }

        [NotMapped]
        public string EdadIngresoCalculada
        {
            get
            {
                if (FechaNacimiento > FechaIngreso) return "Recién nacido";

                var mesesTotal = (FechaIngreso.Year - FechaNacimiento.Year) * 12 + FechaIngreso.Month - FechaNacimiento.Month;
                int dias = FechaIngreso.Day - FechaNacimiento.Day;
                if (dias < 0)
                {
                    mesesTotal--;
                    var mesAnterior = FechaIngreso.AddMonths(-1);
                    dias += DateTime.DaysInMonth(mesAnterior.Year, mesAnterior.Month);
                }

                int anios = mesesTotal / 12;
                int mesesRestantes = mesesTotal % 12;

                if (anios >= 1)
                {
                    return $"{anios} año(s)" + (mesesRestantes > 0 ? $" y {mesesRestantes} mes(es)" : "");
                }
                else
                {
                    return $"{mesesRestantes} mes(es)" + (dias > 0 ? $" y {dias} día(s)" : "");
                }
            }
        }

        [Required(ErrorMessage = "El peso es obligatorio")]
        [Range(0.1, 150.0, ErrorMessage = "El peso debe ser mayor a 0")]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Peso { get; set; }

        [StringLength(255)]
        public string? FotoUrl { get; set; }

        // Relaciones 1:N
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
        public ICollection<VacunaAplicada> VacunasAplicadas { get; set; } = new List<VacunaAplicada>();
        public ICollection<ControlAntiparasitario> ControlesAntiparasitarios { get; set; } = new List<ControlAntiparasitario>();
    }
}
