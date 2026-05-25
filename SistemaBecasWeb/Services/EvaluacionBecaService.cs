using SistemaBecasWeb.Models;

namespace SistemaBecasWeb.Services
{
    public class EvaluacionBecaService : IEvaluacionBecaService
    {
        public SolicitudBeca EvaluarSolicitud(SolicitudBeca solicitud)
        {
            int puntajeTotal = 0;

            // Evaluación por Promedio de Notas 
            if (solicitud.PromedioNotas >= 6.0m && solicitud.PromedioNotas <= 7.0m)
                puntajeTotal += 40;
            else if (solicitud.PromedioNotas >= 5.0m && solicitud.PromedioNotas <= 5.9m)
                puntajeTotal += 30; 
            else if (solicitud.PromedioNotas >= 4.0m && solicitud.PromedioNotas <= 4.9m)
                puntajeTotal += 15; 
            else
                puntajeTotal += 0; 

            // Evaluación por Ingreso Per Cápita 
            // Cálculo: Ingreso familiar / Integrantes del grupo familiar
            int ingresoPerCapita = solicitud.IngresoFamiliar / solicitud.IntegrantesFamilia;

            if (ingresoPerCapita <= 200000)
                puntajeTotal += 40; 
            else if (ingresoPerCapita <= 400000)
                puntajeTotal += 25; 
            else
                puntajeTotal += 10; // Mayor a 400.000 

            // Evaluación por Situación Laboral 
            if (solicitud.SituacionLaboral.Equals("Trabaja", StringComparison.OrdinalIgnoreCase))
                puntajeTotal += 10; // 
            else
                puntajeTotal += 0; // No trabaja 

            // Asignación del Resultado Preliminar 
            solicitud.Puntaje = puntajeTotal;

            if (puntajeTotal >= 70)
                solicitud.Resultado = "Recomendada"; 
            else if (puntajeTotal >= 50 && puntajeTotal <= 69)
                solicitud.Resultado = "En revisión"; 
            else
                solicitud.Resultado = "No recomendada"; 

            return solicitud;
        }
    }
}