using SmartSchool.WebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace SmartSchool.WebAPI.Dto
{
    public class AlunoDto
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        [Required(ErrorMessage = "O campo {0} e obrigatorio.")]
        [MinLength(3, ErrorMessage = "{0} deve ter no minimo 3 caracteres.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} e obrigatorio.")]
        [MinLength(3, ErrorMessage = "{0} deve ter no minimo 3 caracteres.")]
        public string Sobrenome { get; set; }
        [Required(ErrorMessage = "O campo {0} e obrigatorio.")]
        [Phone(ErrorMessage = "O campo {0} esta com o numero invalido.")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O campo {0} e obrigatorio.")]
        public DateTime DataNasc { get; set; } 
        public DateTime DataIni { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
    }
}
