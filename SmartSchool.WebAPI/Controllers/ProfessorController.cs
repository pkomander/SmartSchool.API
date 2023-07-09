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
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessor _professor;
        private readonly IRepository _repository;

        public ProfessorController(IProfessor professor, IRepository repository)
        {
            _professor = professor;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var professores = await _professor.GetProfessores();
            return Ok(professores);
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var professor = await _professor.GetProfessorById(id);

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
                var professor = await _professor.GetProfessorByName(name);

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

                _repository.Add(professor);
                if(await _repository.SaveChanges())
                {
                    return Ok("Professor Cadastrado!");
                }

                return BadRequest("Erro ao tentar adicionar o professor!");
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
                var request = await _professor.GetProfessorById(id);
                if (request == null)
                    return BadRequest("Professor nao encontrado!");

                _repository.Update(professor);
                if (await _repository.SaveChanges())
                {
                    return Ok("Professor Cadastrado!");
                }

                return BadRequest("Erro ao tentar atualizar o professor!");
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
                var request = await _professor.GetProfessorById(id);

                if (request == null)
                    return BadRequest("Professor nao encontrado!");

                _repository.Update(professor);
                if (await _repository.SaveChanges())
                {
                    return Ok("Professor atualizado!");
                }

                return BadRequest("Erro ao tentar atualizar o professor!");
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
                var professor = await _professor.GetProfessorById(id);

                if (professor == null)
                    return BadRequest("Professor nao encontrado!");

                _repository.Delete(professor);
                if (await _repository.SaveChanges())
                {
                    return Ok("professor Deletado!");
                }

                return BadRequest("Erro ao tentar deletar o Professor!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar Professor. Erro: {ex.Message}");
            }

        }
    }
}
