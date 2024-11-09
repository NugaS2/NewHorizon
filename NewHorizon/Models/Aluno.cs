using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHorizon.Models;

public class Aluno : Pessoa
{
    public int IdAluno { get; set; }
    public ICollection<AlunoDisciplina> AlunoDisciplinas { get; set; }
}