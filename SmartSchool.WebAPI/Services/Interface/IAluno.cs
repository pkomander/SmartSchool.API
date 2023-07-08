using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Services.Interface
{
    public interface IAluno
    {
        Task<IEnumerable<Aluno>> GetAlunos();
        Task<Aluno> GetAlunoById(int id);
        Task<IEnumerable<Aluno>> GetAlunoByName(string name);
    }
}
