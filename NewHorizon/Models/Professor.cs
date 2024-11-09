using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewHorizon.Models;

public class Professor : Pessoa
{
    public int IdProfessor { get; set; }
    public ICollection<Disciplina> Disciplinas { get; set; }
}
