using Application.Contratos;
using Application.ManejadorError;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Seguridad
{
    public class Registrar : IRequest<UsuarioData>
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
        }

        public class EjecutaValidador : AbstractValidator<Ejecuta>
        {
            public EjecutaValidador()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly CursosOnlineContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            public Manejador(CursosOnlineContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (existe)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Existe un usuario registrado con este email" });
                }

                var existeUSerName = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();
                if (existeUSerName)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "Existe un usuario registrado con este username" });
                }

                var usuario = new Usuario
                {
                    NombreCompleto = request.Nombre + " " + request.Apellidos,
                    Email = request.Email,
                    UserName = request.Username
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);
                if (resultado.Succeeded)
                    return new UsuarioData
                    {
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario),
                        UserName = usuario.UserName,
                        Email = usuario.Email
                    };

                throw new Exception("No se puedo agergar el nuevo usuario");
            }
        }

    }

}
