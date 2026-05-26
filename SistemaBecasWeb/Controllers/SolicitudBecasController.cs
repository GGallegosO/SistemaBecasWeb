
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaBecasWeb.Models;
using SistemaBecasWeb.Data;
using SistemaBecasWeb.Services;

public class SolicitudBecasController : Controller
{
    private readonly AppDbContext _context;
    private readonly IEvaluacionBecaService _evaluacionService;

    public SolicitudBecasController(AppDbContext context, IEvaluacionBecaService evaluacionService)
    {
        _context = context;
        _evaluacionService = evaluacionService;
    }

    // GET: SOLICITUDBECAS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Solicitudes.ToListAsync());
    }

    // GET: SOLICITUDBECAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var solicitudbeca = await _context.Solicitudes
            .FirstOrDefaultAsync(m => m.IdSolicitud == id);

        if (solicitudbeca == null) return NotFound();

        return View(solicitudbeca);
    }

    // GET: SOLICITUDBECAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SOLICITUDBECAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("IdSolicitud,NombreEstudiante,Rut,Carrera,PromedioNotas,IngresoFamiliar,IntegrantesFamilia,SituacionLaboral")] SolicitudBeca solicitudbeca)
    {

        // Con estas líneas le decimos a .NET que NO valide estos campos porque los calculamos nosotros
        ModelState.Remove("Puntaje");
        ModelState.Remove("Resultado");
        ModelState.Remove("EstadoSolicitud");
        ModelState.Remove("FechaSolicitud");


        if (ModelState.IsValid)
        {
            // El motor matemático calcula automáticamente el Puntaje y el Resultado
            solicitudbeca = _evaluacionService.EvaluarSolicitud(solicitudbeca);

            // Nota: La FechaSolicitud y el EstadoSolicitud ("Pendiente") se inicializan solos.

            _context.Add(solicitudbeca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(solicitudbeca);
    }

    // GET: SOLICITUDBECAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var solicitudbeca = await _context.Solicitudes.FindAsync(id);

        if (solicitudbeca == null) return NotFound();

        return View(solicitudbeca);
    }

    // POST: SOLICITUDBECAS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("IdSolicitud,NombreEstudiante,Rut,Carrera,PromedioNotas,IngresoFamiliar,IntegrantesFamilia,SituacionLaboral,Puntaje,Resultado,EstadoSolicitud,FechaSolicitud")] SolicitudBeca solicitudbeca)
    {
        if (id != solicitudbeca.IdSolicitud) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                // Si cambian notas o ingresos, el motor recalcula el puntaje automáticamente
                solicitudbeca = _evaluacionService.EvaluarSolicitud(solicitudbeca);

                _context.Update(solicitudbeca);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudBecaExists(solicitudbeca.IdSolicitud)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(solicitudbeca);
    }

    // GET: SOLICITUDBECAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var solicitudbeca = await _context.Solicitudes
            .FirstOrDefaultAsync(m => m.IdSolicitud == id);

        if (solicitudbeca == null) return NotFound();

        return View(solicitudbeca);
    }

    // POST: SOLICITUDBECAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var solicitudbeca = await _context.Solicitudes.FindAsync(id);
        if (solicitudbeca != null)
        {
            _context.Solicitudes.Remove(solicitudbeca);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SolicitudBecaExists(int? idsolicitud)
    {
        return _context.Solicitudes.Any(e => e.IdSolicitud == idsolicitud);
    }
}
