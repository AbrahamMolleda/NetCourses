using Application.ManejadorError;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seguridad
{
    public class UsuarioRolAgregar
    {
        public class Ejecuta : IRequest
        {
            public string Username { get; set; }
            public string RolNombre { get; set; }
        }

        public class EjecutaValidador : AbstractValidator<Ejecuta>
        {
            public EjecutaValidador()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.RolNombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RolNombre);
                if (role == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { mensaje = "El rol no existe" });
                }

                var usuarioIdentity = await _userManager.FindByNameAsync(request.Username);
                if (usuarioIdentity == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { mensaje = "El usuario no existe" });
                }

                var resultado = await _userManager.AddToRoleAsync(usuarioIdentity, request.RolNombre);
                if (resultado.Succeeded)
                {
                    return Unit.Value;
                }

                throw new Exception("No se puede agregar el rol al usuario");
            }
        }
    }
}
