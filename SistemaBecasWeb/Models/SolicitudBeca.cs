using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaBecasWeb.Models
{
    [Table("SolicitudesBeca")] 
    public class SolicitudBeca
    {
        [Key]
        public int IdSolicitud { get; set; }

        [Required(ErrorMessage = "El nombre del estudiante es obligatorio.")]
        [StringLength(100)]
        public string NombreEstudiante { get; set; }

        [Required(ErrorMessage = "El RUT es obligatorio.")]
        [StringLength(12)]
        public string Rut { get; set; }

        [Required(ErrorMessage = "La carrera es obligatoria.")]
        [StringLength(100)]
        public string Carrera { get; set; }

        [Required(ErrorMessage = "El promedio de notas es obligatorio.")]
        [Range(1.0, 7.0, ErrorMessage = "El promedio debe estar entre 1.0 y 7.0.")]
        public decimal PromedioNotas { get; set; }

        [Required(ErrorMessage = "El ingreso familiar es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El ingreso familiar debe ser mayor o igual a 0.")]
        public int IngresoFamiliar { get; set; }

        [Required(ErrorMessage = "La cantidad de integrantes es obligatoria.")]
        [Range(1, 30, ErrorMessage = "Debe haber al menos 1 integrante en la familia.")]
        public int IntegrantesFamilia { get; set; }

        [Required(ErrorMessage = "La situación laboral es obligatoria.")]
        [StringLength(20)]
        public string SituacionLaboral { get; set; } // "Trabaja" o "No trabaja"

        // ---- Los siguientes campos son calculados o asignados por el sistema, no por el usuario en el formulario ----

        public int Puntaje { get; set; }

        [StringLength(30)]
        public string Resultado { get; set; }

        [StringLength(30)]
        public string EstadoSolicitud { get; set; } = "Pendiente"; // Por defecto arranca en Pendiente

        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
    }
}