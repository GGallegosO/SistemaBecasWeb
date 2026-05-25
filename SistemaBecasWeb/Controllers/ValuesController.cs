using Microsoft.AspNetCore.Mvc;
using SistemaBecasWeb.Models;
using SistemaBecasWeb.Services;

namespace SistemaBecasWeb.Controllers
{
    [Route("api/becas")]
    [ApiController]
    public class BecasApiController : ControllerBase
    {
        private readonly IEvaluacionBecaService _evaluacionService;

        // Inyectamos nuestro servicio (el motor matemático)
        public BecasApiController(IEvaluacionBecaService evaluacionService)
        {
            _evaluacionService = evaluacionService;
        }

        [HttpPost("evaluar")]
        public IActionResult Evaluar([FromBody] EvaluacionRequestDto datos)
        {
            // Armamos una solicitud temporal solo con los datos necesarios para el cálculo
            var solicitudTemporal = new SolicitudBeca
            {
                PromedioNotas = datos.PromedioNotas,
                IngresoFamiliar = datos.IngresoFamiliar,
                IntegrantesFamilia = datos.IntegrantesFamilia,
                SituacionLaboral = datos.SituacionLaboral
            };

            // Pasamos la solicitud por nuestro motor de evaluación
            var resultado = _evaluacionService.EvaluarSolicitud(solicitudTemporal);

            // Devolvemos la respuesta exacta que pide el documento
            return Ok(new
            {
                puntaje = resultado.Puntaje,
                resultado = resultado.Resultado
            });
        }
    }
}