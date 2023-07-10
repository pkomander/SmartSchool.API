using AutoMapper;
using SmartSchool.WebAPI.Dto;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Profiles
{
    public class SmartProfile : Profile
    {
        public SmartProfile()
        {
            CreateMap<Aluno, AlunoDto>().ReverseMap();
            CreateMap<Curso, CursoDto>().ReverseMap();
            CreateMap<Disciplina, DisciplinaDto>().ReverseMap();
            CreateMap<Professor, ProfessorDto>().ReverseMap();
        }
    }
}
