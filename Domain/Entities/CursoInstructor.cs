using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CursoInstructor
    {
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public int InstrcutorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
