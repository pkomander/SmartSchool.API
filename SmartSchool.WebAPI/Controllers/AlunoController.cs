using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Services.Interface;

namespace SmartSchool.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IRepository _repo;
        private readonly IAluno _aluno;
        public AlunoController(DataContext context, IRepository repo, IAluno aluno)
        {
            _context = context;
            _repo = repo;
            _aluno = aluno;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var alunos = await _context.Alunos.ToListAsync();
            var alunos = await _aluno.GetAlunos();
            return Ok(alunos);
        }

        [HttpGet("byId{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aluno = await _aluno.GetAlunoById(id);

            if (aluno == null)
                return BadRequest("Aluno nao encontrado!");

            return Ok(aluno);
        }

        [HttpGet("ByName/{nome}")]
        public async Task<IActionResult> GetByName(string nome)
        {
            //var aluno = await _context.Alunos.FirstOrDefaultAsync(a => a.Nome.Contains(nome) || a.Sobrenome.Contains(nome));
            var aluno = await _aluno.GetAlunoByName(nome);

            if (aluno == null)
                return BadRequest("Aluno nao encontrado!");

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Aluno aluno)
        {
            try
            {
                if (aluno == null)
                    return BadRequest("Objeto Aluno esta null");

                _repo.Add(aluno);
                if(await _repo.SaveChanges())
                {
                    return Ok("Aluno Cadastrado!");
                }
                return BadRequest("Aluno nao cadastrado!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar Alunos. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Aluno aluno)
        {
            try
            {
                var request = await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (request == null)
                    return BadRequest("Aluno nao encontrado!");

                //mapeando objeto
                request.Nome = aluno.Nome;
                request.Sobrenome = aluno.Sobrenome;
                request.Telefone = aluno.Telefone;

                _repo.Update(request);
                if (await _repo.SaveChanges())
                {
                    return Ok("Atualizado com sucesso!");
                }

                return BadRequest("Erro ao atualizar aluno!");                
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar aluno. Erro: {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, Aluno aluno)
        {
            try
            {
                //AsNoTracking() - e utilizado para nao bloquear o select para modificacoes no objeto
                //como nos metodos de update e patch, podendo alterar o estado do objeto sem a necessidade de travalo
                var request = await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                if (request == null)
                    return BadRequest("Aluno nao encontrado!");

                _repo.Update(request);
                if (await _repo.SaveChanges())
                {
                    return Ok("Atualizado com sucesso!");
                }
                return BadRequest("Erro ao atualizar aluno");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar aluno. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var aluno = await _context.Alunos.FirstOrDefaultAsync(x => x.Id == id);

                if (aluno == null)
                    return BadRequest("Aluno nao encontrado!");

                _repo.Delete(aluno);
                if (await _repo.SaveChanges())
                {
                    return Ok("Deletado com sucesso!");
                }

                return BadRequest("Erro ao deletar aluno!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar aluno. Erro: {ex.Message}");
            }
            
        }
    }
}
