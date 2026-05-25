using Microsoft.EntityFrameworkCore;
using SistemaBecasWeb.Models;

namespace SistemaBecasWeb.Data
{
    public class AppDbContext : DbContext
    {
        // El constructor que recibe la configuración desde Program.cs
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Aquí le decimos que nuestra clase SolicitudBeca representa la tabla SolicitudesBeca
        public DbSet<SolicitudBeca> Solicitudes { get; set; }
    }
}