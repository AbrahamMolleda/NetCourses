﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Curso
    {
        public Guid CursoId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public byte[] FotoPortada { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public Precio PrecioPromocion { get; set; }
        public ICollection<Comentario> ComentarioLista { get; set; }
        public ICollection<CursoInstructor> InstructoresLink { get; set; }
    }
}
