using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly DataContext _context;

        public ProfessorController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Professores.ToListAsync());
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var professor = await _context.Professores.FirstOrDefaultAsync(x => x.Id == id);

                if (professor == null)
                {
                    return BadRequest("Professor nao encontrado!");
                }

                return Ok(professor);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar professor. Erro: {ex.Message}");
            }
        }

        [HttpGet("byName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var professor = await _context.Professores.FirstOrDefaultAsync(x => x.Nome.Contains(name));

                if (professor == null)
                {
                    return BadRequest("Professor nao encontrado!");
                }

                return Ok(professor);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar professor. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Professor professor)
        {
            try
            {
                if (professor == null)
                    return BadRequest("Objeto Professor esta null");

                _context.Professores.Add(professor);
                await _context.SaveChangesAsync();

                return Ok(professor);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Professor. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Professor professor)
        {
            try
            {
                var request = await _context.Professores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (request == null)
                    return BadRequest("Professor nao encontrado!");

                _context.Professores.Update(professor);
                await _context.SaveChangesAsync();

                return Ok("Atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar professor. Erro: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, Professor professor)
        {
            try
            {
                //AsNoTracking() - e utilizado para nao bloquear o select para modificacoes no objeto
                //como nos metodos de update e patch, podendo alterar o estado do objeto sem a necessidade de travalo
                var request = await _context.Professores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                if (request == null)
                    return BadRequest("Professor nao encontrado!");

                _context.Professores.Update(request);
                await _context.SaveChangesAsync();

                return Ok("Atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar professor. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var professor = await _context.Professores.FirstOrDefaultAsync(x => x.Id == id);

                if (professor == null)
                    return BadRequest("Professor nao encontrado!");

                _context.Professores.Remove(professor);
                await _context.SaveChangesAsync();

                return Ok("Professor deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar Professor. Erro: {ex.Message}");
            }

        }
    }
}
