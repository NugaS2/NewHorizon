using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHorizon.Models;

public partial class Disciplina
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public ICollection<AlunoDisciplina> AlunoDisciplinas { get; set; }

    public int ProfessorId { get; set; }
    public Professor Professor { get; set; }
}
