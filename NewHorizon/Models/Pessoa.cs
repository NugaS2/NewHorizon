using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace NewHorizon.Models;

public abstract class Pessoa
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Cpf { get; set; }

    public string Email { get; set; }

    public DateTime Nascimento { get; set; }

}