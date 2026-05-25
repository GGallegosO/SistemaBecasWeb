
using SistemaBecasWeb.Models;

namespace SistemaBecasWeb.Services
{

    /// <summary>
    /// Actúa como un contrato. Obliga a que cualquier clase que lo implemente
    /// tenga el algoritmo de evaluación.
    /// Es la pieza clave que permite conectar (inyectar) la lógica de negocio
    /// en los controladores sin acoplarlos directamente.
    /// </summary>


    public interface IEvaluacionBecaService
    {

        SolicitudBeca EvaluarSolicitud(SolicitudBeca solicitud);

    }
}
