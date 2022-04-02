using MediatR;
using Persistence.DapperConexion.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Instructores
{
    public class Consulta
    {
        public class Lista : IRequest<List<InstructorModel>> { }

        public class Manejador : IRequestHandler<Lista, List<InstructorModel>>
        {
            private readonly IInstructorRepository _instructorRepository;
            public Manejador(IInstructorRepository instructorRepository)
            {
                _instructorRepository = instructorRepository;
            }
            public async Task<List<InstructorModel>> Handle(Lista request, CancellationToken cancellationToken)
            {
                var result = await _instructorRepository.ObtenerLista();
                return result.ToList();
            }
        }
    }
}
