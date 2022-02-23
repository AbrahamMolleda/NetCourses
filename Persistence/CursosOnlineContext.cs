using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class CursosOnlineContext : DbContext
    {
        public CursosOnlineContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CursoInstructor>().HasKey(ci => new { ci.InstrcutorId, ci.CursoId });
        }
    }
}
