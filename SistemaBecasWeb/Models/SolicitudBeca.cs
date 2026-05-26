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
        [Display(Name = "Nombre Completo")]
        [StringLength(100)]
        public string NombreEstudiante { get; set; }

        [Required(ErrorMessage = "El RUT es obligatorio.")]
        [Display(Name = "RUT")]
        [RegularExpression(@"^\d{7,8}-[0-9Kk]$", ErrorMessage = "El formato debe ser 12345678-9 o 12345678-K (incluya el guion)")]
        [StringLength(12)]
        public string Rut { get; set; }

        [Required(ErrorMessage = "La carrera es obligatoria.")]
        [Display(Name = "Carrera")]
        [StringLength(100)]
        public string Carrera { get; set; }

        [Required(ErrorMessage = "El promedio de notas es obligatorio.")]
        [Display(Name = "Promedio de Notas")]
        [Range(1.0, 7.0, ErrorMessage = "El promedio debe estar entre 1.0 y 7.0.")]
        public decimal PromedioNotas { get; set; }

        [Required(ErrorMessage = "El ingreso familiar es obligatorio.")]
        [Display(Name = "Ingreso Familiar Mensual")]
        [Range(0, int.MaxValue, ErrorMessage = "El ingreso familiar debe ser mayor o igual a 0.")]
        public int IngresoFamiliar { get; set; }

        [Required(ErrorMessage = "La cantidad de integrantes es obligatoria.")]
        [Display(Name = "Cantidad de Integrantes")]
        [Range(1, 30, ErrorMessage = "Debe haber al menos 1 integrante en la familia.")]
        public int IntegrantesFamilia { get; set; }

        [Required(ErrorMessage = "La situación laboral es obligatoria.")]
        [Display(Name = "Situación Laboral")]
        [StringLength(20)]
        public string SituacionLaboral { get; set; } // "Trabaja" o "No trabaja"

        // ---- Los siguientes campos son calculados o asignados por el sistema, no por el usuario en el formulario ----

        [Display(Name = "Puntaje Obtenido")]
        public int Puntaje { get; set; }

        [Display(Name = "Resultado Preliminar")]
        [StringLength(30)]
        public string Resultado { get; set; }

        [Display(Name = "Estado de la Solicitud")]
        [StringLength(30)]
        public string EstadoSolicitud { get; set; } = "Pendiente"; // Por defecto arranca en Pendiente

        [Display(Name = "Fecha de Postulación")]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
    }
}