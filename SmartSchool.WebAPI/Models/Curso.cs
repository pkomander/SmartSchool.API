namespace SmartSchool.WebAPI.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}
