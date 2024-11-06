using System;
using System.Collections.Generic;

namespace NewHorizon.Models;

public partial class Disciplina
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();

    public virtual ICollection<Professores> Professores { get; set; } = new List<Professores>();
}
