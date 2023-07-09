using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Services.Interface
{
    public interface IProfessor
    {
        Task<IEnumerable<Professor>> GetProfessores();
        Task<Professor> GetProfessorById(int id);
        Task<IEnumerable<Professor>> GetProfessorByName(string name);
    }
}
