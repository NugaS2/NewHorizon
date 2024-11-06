using System;
using System.Collections.Generic;

namespace NewHorizon.Models;

public partial class Aluno
{
    public int Id { get; set; }

    public int IdPessoa { get; set; }

    public int IdDisciplina { get; set; }

    public virtual Disciplina IdDisciplinaNavigation { get; set; } = null!;

    public virtual Pessoa IdPessoaNavigation { get; set; } = null!;
}
