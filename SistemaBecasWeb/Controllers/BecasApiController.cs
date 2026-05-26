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

        public BecasApiController(IEvaluacionBecaService evaluacionService)
        {
            _evaluacionService = evaluacionService;
        }

        [HttpPost("evaluar")]
        public IActionResult Evaluar([FromBody] EvaluacionRequest request)
        {
            // Armamos un objeto temporal forzando el tipo (int) en el ingreso por si tu modelo lo requiere
            var solicitudTemporal = new SolicitudBeca
            {
                PromedioNotas = (decimal)request.PromedioNotas,
                IngresoFamiliar = (int)request.IngresoFamiliar, 
                IntegrantesFamilia = request.IntegrantesFamilia,
                SituacionLaboral = request.SituacionLaboral
            };

            var resultado = _evaluacionService.EvaluarSolicitud(solicitudTemporal);

            return Ok(new
            {
                puntaje = resultado.Puntaje,
                resultado = resultado.Resultado
            });
        }
    }

    public class EvaluacionRequest
    {
        public decimal PromedioNotas { get; set; }
        public decimal IngresoFamiliar { get; set; }
        public int IntegrantesFamilia { get; set; }
        public string SituacionLaboral { get; set; }
    }
}