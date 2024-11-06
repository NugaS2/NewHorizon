using System;
using System.Collections.Generic;

namespace NewHorizon.Models;

public partial class Pessoa
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly Nascimento { get; set; }

    public virtual ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();

    public virtual ICollection<Professores> Professores { get; set; } = new List<Professores>();
}
