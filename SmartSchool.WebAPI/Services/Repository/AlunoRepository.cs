using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Services.Interface;

namespace SmartSchool.WebAPI.Services.Repository
{
    public class AlunoRepository : IAluno
    {
        private readonly DataContext _context;
        public AlunoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            IEnumerable<Aluno> alunos = await _context.Alunos.ToListAsync();
            return alunos;
        }

        public async Task<Aluno> GetAlunoById(int id)
        {
            try
            {
                var aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Id == id);

                if (aluno == null)
                    return null;

                return aluno;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IEnumerable<Aluno>> GetAlunoByName(string name)
        {
            try
            {
                IEnumerable<Aluno> alunos = await _context.Alunos.Where(x => x.Nome == name || x.Sobrenome == name).ToListAsync();

                if (alunos == null)
                    return null;

                return alunos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
