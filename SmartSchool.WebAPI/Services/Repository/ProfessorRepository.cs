using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Services.Interface;

namespace SmartSchool.WebAPI.Services.Repository
{
    public class ProfessorRepository : IProfessor
    {
        private readonly DataContext _context;
        public ProfessorRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Professor> GetProfessorById(int id)
        {
            try
            {
                var professor = await _context.Professores.FirstOrDefaultAsync(x => x.Id == id);

                if (professor == null)
                    return null;

                return professor;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Professor>> GetProfessorByName(string name)
        {
            try
            {
                IEnumerable<Professor> professores = await _context.Professores.Where(x => x.Nome == name).ToListAsync();

                if (professores == null)
                    return null;

                return professores;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Professor>> GetProfessores()
        {
            IEnumerable<Professor> professores = await _context.Professores.ToListAsync();
            return professores;
        }
    }
}
