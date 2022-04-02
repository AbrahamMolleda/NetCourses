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
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly IInstructorRepository _instructorRepository;
            public Manejador(IInstructorRepository instructorRepository)
            {
                _instructorRepository = instructorRepository;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var resultado = await _instructorRepository.Elimina(request.Id);
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se puede eliminar el instructor");
            }
        }
    }
}
