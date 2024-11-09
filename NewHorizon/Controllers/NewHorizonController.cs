using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewHorizon.Models;

namespace NewHorizon.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewHorizonController : ControllerBase
    {
        private readonly ILogger<NewHorizonController> _logger;
        private readonly MasterContext _context;

        public class AlunoDto
        {
            public int IdAluno { get; set; }
            public string Nome { get; set; }
            public List<DisciplinaDto> Disciplinas { get; set; }
        }

        public class DisciplinaDto
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }


        public NewHorizonController(ILogger<NewHorizonController> logger, MasterContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("Disciplinas", Name = "GetDisciplinas")]
        public ActionResult<IEnumerable<Disciplina>> GetDisciplinas()
        {
            return Ok(_context.Disciplinas.ToList());
        }

        [HttpGet("Disciplinas/{id}", Name = "GetDisciplinaById")]
        public ActionResult<Disciplina> GetDisciplinaById(int id)
        {
            var disciplina = _context.Disciplinas.Find(id);
            if (disciplina == null)
                return NotFound($"Disciplina com ID {id} não encontrada.");

            return Ok(disciplina);
        }
       
        [HttpPost("Disciplinas", Name = "CreateDisciplina")]
        public ActionResult<Disciplina> CreateDisciplina([FromBody] Disciplina disciplina)
        {
            if (disciplina == null)
                return BadRequest("Dados inválidos.");

            _context.Disciplinas.Add(disciplina);
            _context.SaveChanges();

            return CreatedAtRoute("GetDisciplinaById", new { id = disciplina.Id }, disciplina);
        }

        [HttpPut("Disciplinas/{id}", Name = "UpdateDisciplina")]
        public ActionResult UpdateDisciplina(int id, [FromBody] Disciplina disciplina)
        {
            if (id != disciplina.Id)
                return BadRequest("ID da disciplina não corresponde ao ID fornecido.");

            var existingDisciplina = _context.Disciplinas.Find(id);
            if (existingDisciplina == null)
                return NotFound($"Disciplina com ID {id} não encontrada.");

            existingDisciplina.Nome = disciplina.Nome;
            existingDisciplina.ProfessorId = disciplina.ProfessorId;

            _context.Disciplinas.Update(existingDisciplina);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("Disciplinas/{id}", Name = "DeleteDisciplina")]
        public ActionResult DeleteDisciplina(int id)
        {
            var disciplina = _context.Disciplinas.Find(id);
            if (disciplina == null)
                return NotFound($"Disciplina com ID {id} não encontrada.");

            _context.Disciplinas.Remove(disciplina);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("Professores", Name = "GetProfessores")]
        public ActionResult<IEnumerable<Professor>> GetProfessores()
        {
            return Ok(_context.Professores.ToList());
        }

        [HttpGet("Professores/{id}", Name = "GetProfessorById")]
        public ActionResult<Disciplina> GetProfessorById(int id)
        {
            var professor = _context.Professores.Find(id);
            if (professor == null)
                return NotFound($"Professor com ID {id} não encontrado.");

            return Ok(professor);
        }
       
        [HttpPost("Professores", Name = "CreateProfessor")]
        public ActionResult<Disciplina> CreateDisciplina([FromBody] Professor professor)
        {
            if (professor == null)
                return BadRequest("Dados inválidos.");

            _context.Professores.Add(professor);
            _context.SaveChanges();

            return CreatedAtRoute("GetProfessorById", new { id = professor.Id }, professor);
        }

        [HttpPut("Professores/{id}", Name = "UpdateProfessor")]
        public ActionResult UpdateProfessor(int id, [FromBody] Professor professor)
        {
            if (id != professor.Id)
                return BadRequest("ID do professor não corresponde ao ID fornecido.");

            var existingProfessor = _context.Professores.Find(id);
            if (existingProfessor == null)
                return NotFound($"Professor com ID {id} não encontrado.");

            existingProfessor.Nome = professor.Nome;
            existingProfessor.Email = professor.Email;
            existingProfessor.Cpf = professor.Cpf;
            existingProfessor.Nascimento = professor.Nascimento;

            _context.Professores.Update(existingProfessor);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("Professor/{id}", Name = "DeleteProfessor")]
        public ActionResult DeleteProfessor(int id)
        {
            var professor = _context.Professores.Find(id);
            if (professor == null)
                return NotFound($"Professor com ID {id} não encontrado.");

            _context.Professores.Remove(professor);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("Alunos", Name = "GetAlunos")]
        public ActionResult<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                var alunos = _context.Alunos
                    .Include(a => a.AlunoDisciplinas)
                        .ThenInclude(ad => ad.Disciplina)
                    .ToList();

                if (!alunos.Any())
                    return NotFound("No Alunos found.");

                var alunoDtos = alunos.Select(aluno => new AlunoDto
                {
                    IdAluno = aluno.IdAluno,
                    Nome = aluno.Nome,
                    Disciplinas = aluno.AlunoDisciplinas.Select(ad => new DisciplinaDto
                    {
                        Id = ad.Disciplina.Id,
                        Nome = ad.Disciplina.Nome
                    }).ToList()
                }).ToList();

                return Ok(alunoDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Alunos/{id}", Name = "GetAlunoById")]
        public ActionResult<Aluno> GetAlunoById(int id)
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno == null)
                return NotFound($"Aluno com ID {id} não encontrado.");

            return Ok(aluno);
        }
       
        [HttpPost("Alunos", Name = "CreateAluno")]
        public ActionResult<Aluno> CreateAluno([FromBody] Aluno aluno)
        {
            if (aluno == null)
                return BadRequest("Dados inválidos.");

            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            return CreatedAtRoute("GetDisciplinaById", new { id = aluno.Id }, aluno);
        }

        [HttpPut("Alunos/{id}", Name = "UpdateAluno")]
        public ActionResult UpdateAluno(int id, [FromBody] Aluno aluno)
        {
            if (id != aluno.Id)
                return BadRequest("ID da disciplina não corresponde ao ID fornecido.");

            var existingAluno = _context.Alunos.Find(id);
            if (existingAluno == null)
                return NotFound($"Aluno com ID {id} não encontrado.");

            existingAluno.Nome = aluno.Nome;
            existingAluno.Email = aluno.Email;
            existingAluno.Cpf = aluno.Cpf;
            existingAluno.Nascimento = aluno.Nascimento;
            _context.Alunos.Update(existingAluno);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("Alunos/{id}", Name = "DeleteAluno")]
        public ActionResult DeleteAluno(int id)
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno == null)
                return NotFound($"Aluno com ID {id} não encontrado.");

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
