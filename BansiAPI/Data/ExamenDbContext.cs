using Microsoft.EntityFrameworkCore;
using WsApiexamen.Models.Entities;

namespace WsApiexamen.Data
{
    public class ExamenDbContext:DbContext
    {
        public ExamenDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<tblExamen> Examenes { get; set; }
    }
}
