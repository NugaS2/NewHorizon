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

    public virtual DbSet<Pessoa> Pessoas { get; set; }

    public virtual DbSet<Professores> Professores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=RIKA\\\\\\\\SQLEXPRESS,55061;Database=master;User Id=NugaS2;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ALUNOS__3214EC271676B90C");

            entity.ToTable("ALUNOS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdDisciplina).HasColumnName("ID_DISCIPLINA");
            entity.Property(e => e.IdPessoa).HasColumnName("ID_PESSOA");

            entity.HasOne(d => d.IdDisciplinaNavigation).WithMany(p => p.Alunos)
                .HasForeignKey(d => d.IdDisciplina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DISCIPLINAS_ALUNOS");

            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.Alunos)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ALUNOS_PESSOAS");
        });

        modelBuilder.Entity<Disciplina>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DISCIPLI__3214EC270501A277");

            entity.ToTable("DISCIPLINAS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOME");
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PESSOAS__3214EC2759E92055");

            entity.ToTable("PESSOAS");

            entity.HasIndex(e => e.Cpf, "UQ__PESSOAS__C1F89731EFBACC9B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CPF");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Nascimento).HasColumnName("NASCIMENTO");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOME");
        });

        modelBuilder.Entity<Professores>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PROFESSO__3214EC27636701DD");

            entity.ToTable("PROFESSORES");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdDisciplina).HasColumnName("ID_DISCIPLINA");
            entity.Property(e => e.IdPessoa).HasColumnName("ID_PESSOA");

            entity.HasOne(d => d.IdDisciplinaNavigation).WithMany(p => p.Professores)
                .HasForeignKey(d => d.IdDisciplina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DISCIPLINAS_PROFESSORES");

            entity.HasOne(d => d.IdPessoaNavigation).WithMany(p => p.Professores)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROFESSORES_PESSOAS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
