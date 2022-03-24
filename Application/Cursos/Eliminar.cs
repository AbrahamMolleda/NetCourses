using Application.ManejadorError;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var instructoresDb = _context.CursoInstructor.Where(i => i.CursoId == request.Id);
                foreach(var instructor in instructoresDb)
                {
                    _context.CursoInstructor.Remove(instructor);
                }

                var comentariosDb = _context.Comentario.Where(x => x.CursoId == request.Id);
                foreach (var cmt in instructoresDb)
                {
                    _context.CursoInstructor.Remove(cmt);
                }

                var precioDb = _context.Precio.Where(x => x.CursoId == request.Id).FirstOrDefault();
                if(precioDb != null)
                {
                    _context.Precio.Remove(precioDb);
                }

                var curso = await _context.Curso.FindAsync(request.Id);
                if (curso == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "no se encontro el curso" });

                _context.Remove(curso);
                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                    return Unit.Value;

                throw new Exception("No se pudieron guardar los cambios");
            }
        }
    }
}
