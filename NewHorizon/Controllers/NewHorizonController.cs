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

        public NewHorizonController(ILogger<NewHorizonController> logger, MasterContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("Disciplinas", Name = "GetDisciplinas")]
        public IEnumerable<Disciplina> GetDisciplinas()
        {
            return _context.Disciplinas.Include(d => d.Alunos).Include(d => d.Professores).ToList();
        }
       
    }
}
