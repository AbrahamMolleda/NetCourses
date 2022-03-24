using Application.ManejadorError;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDto>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, CursoDto>
        {
            private readonly CursosOnlineContext _context;
            private readonly IMapper _mapper;
            public Manejador(CursosOnlineContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CursoDto> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso
                    .Include(x => x.ComentarioLista)
                    .Include(x => x.PrecioPromocion)
                    .Include(x => x.InstructoresLink)
                    .ThenInclude(x => x.Instructor)
                    .FirstOrDefaultAsync(c => c.CursoId == request.Id);

                if (curso == null)
                    //throw new Exception("No se puede eliminar el curso");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "no se encontro el curso" });

                var cursoDto = _mapper.Map<Curso, CursoDto>(curso);
                return cursoDto;
            }
        }
    }
}
