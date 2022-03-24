using Application.Cursos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curso, CursoDto>()
                .ForMember(dto => dto.Instructores, opt => opt.MapFrom(src => src.InstructoresLink.Select(ins => ins.Instructor).ToList()))
                .ForMember(dto => dto.Comentarios, opt => opt.MapFrom(src => src.ComentarioLista))
                .ForMember(dto => dto.Precio, opt => opt.MapFrom(src => src.PrecioPromocion));
            CreateMap<CursoInstructor, CursoInstructorDto>();
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Comentario, ComentarioDto>();
            CreateMap<Precio, PrecioDto>();
        }
    }
}
