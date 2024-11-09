using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NewHorizon.Models;

public partial class MasterContext : DbContext
{
    public MasterContext()
    {
    }

    public MasterContext(DbContextOptions<MasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aluno> Alunos { get; set; }

    public virtual DbSet<Disciplina> Disciplinas { get; set; }

    public virtual DbSet<Professor> Professores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=RIKA\\\\\\\\SQLEXPRESS,55061;Database=master;User Id=NugaS2;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>().ToTable("Pessoas");
        modelBuilder.Entity<Aluno>().ToTable("Alunos");
        modelBuilder.Entity<Professor>().ToTable("Professores");
        modelBuilder.Entity<AlunoDisciplina>()
            .HasKey(ad => new { ad.AlunoId, ad.DisciplinaId });
        modelBuilder.Entity<AlunoDisciplina>()
            .HasOne(ad => ad.Aluno)
            .WithMany(a => a.AlunoDisciplinas)
            .HasForeignKey(ad => ad.AlunoId)
            .OnDelete(DeleteBehavior.Restrict); 
        modelBuilder.Entity<AlunoDisciplina>()
            .HasOne(ad => ad.Disciplina)
            .WithMany(d => d.AlunoDisciplinas)
            .HasForeignKey(ad => ad.DisciplinaId)
            .OnDelete(DeleteBehavior.Restrict); 
        modelBuilder.Entity<Disciplina>()
            .HasOne(d => d.Professor)
            .WithMany(p => p.Disciplinas)
            .HasForeignKey(d => d.ProfessorId)
            .OnDelete(DeleteBehavior.Restrict); 
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
