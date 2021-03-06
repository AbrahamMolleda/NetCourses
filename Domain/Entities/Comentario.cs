using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comentario
    {
        public Guid ComentarioId { get; set; }
        public string Alumno { get; set; }
        public int Puntaje { get; set; }
        public string ComentarioLista { get; set; }
        public Guid CursoId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public Curso Curso { get; set; }
    }
}
