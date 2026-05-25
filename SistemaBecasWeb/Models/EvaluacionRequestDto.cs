namespace SistemaBecasWeb.Models
{
    // Esta clase solo sirve para transportar los 4 datos que pide el endpoint de evaluación
    public class EvaluacionRequestDto
    {
        public decimal PromedioNotas { get; set; }
        public int IngresoFamiliar { get; set; }
        public int IntegrantesFamilia { get; set; }
        public string SituacionLaboral { get; set; }
    }
}