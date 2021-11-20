using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebPortalCadAlu.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebPortalCadAlu.Data
{
    public partial class cadaluContext : DbContext
    {
        public cadaluContext()
        {
        }

        public cadaluContext(DbContextOptions<cadaluContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agrupamentos> Agrupamentos { get; set; }
        public virtual DbSet<Alunos> Alunos { get; set; }
        public virtual DbSet<Avaliacoes> Avaliacoes { get; set; }
        public virtual DbSet<Disciplinas> Disciplinas { get; set; }
        public virtual DbSet<Escolas> Escolas { get; set; }
        public virtual DbSet<Mensagens> Mensagens { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Professores> Professores { get; set; }
        public virtual DbSet<Sumario> Sumario { get; set; }
        public virtual DbSet<Turmas> Turmas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=cadalu;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agrupamentos>(entity =>
            {
                entity.ToTable("agrupamentos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Alunos>(entity =>
            {
                entity.ToTable("alunos");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .IsUnicode(false);

                entity.Property(e => e.Pai1).HasColumnName("pai1");

                entity.Property(e => e.Pai2).HasColumnName("pai2");

                entity.Property(e => e.Turma).HasColumnName("turma");

                entity.HasOne(d => d.Pai1Navigation)
                    .WithMany(p => p.AlunosPai1Navigation)
                    .HasForeignKey(d => d.Pai1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_alunos_pais");

                entity.HasOne(d => d.Pai2Navigation)
                    .WithMany(p => p.AlunosPai2Navigation)
                    .HasForeignKey(d => d.Pai2)
                    .HasConstraintName("FK_alunos_pais1");

                entity.HasOne(d => d.TurmaNavigation)
                    .WithMany(p => p.Alunos)
                    .HasForeignKey(d => d.Turma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_alunos_turmas");
            });

            modelBuilder.Entity<Avaliacoes>(entity =>
            {
                entity.ToTable("avaliacoes");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Aluno).HasColumnName("aluno");

                entity.Property(e => e.Aval)
                    .IsRequired()
                    .HasColumnName("aval")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Avaliador).HasColumnName("avaliador");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnName("tipo")
                    .IsUnicode(false);

                entity.HasOne(d => d.AlunoNavigation)
                    .WithMany(p => p.Avaliacoes)
                    .HasForeignKey(d => d.Aluno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_avaliacoes_alunos");

                entity.HasOne(d => d.AvaliadorNavigation)
                    .WithMany(p => p.Avaliacoes)
                    .HasForeignKey(d => d.Avaliador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_avaliacoes_professores");
            });

            modelBuilder.Entity<Disciplinas>(entity =>
            {
                entity.HasKey(e => new { e.Turma, e.Professor })
                    .HasName("PK__discipli__6738C29B4C3EB63D");

                entity.ToTable("disciplinas");

                entity.Property(e => e.Turma).HasColumnName("turma");

                entity.Property(e => e.Professor).HasColumnName("professor");

                entity.HasOne(d => d.ProfessorNavigation)
                    .WithMany(p => p.Disciplinas)
                    .HasForeignKey(d => d.Professor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_disciplinas_professores");

                entity.HasOne(d => d.TurmaNavigation)
                    .WithMany(p => p.Disciplinas)
                    .HasForeignKey(d => d.Turma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_disciplinas_turmas");
            });

            modelBuilder.Entity<Escolas>(entity =>
            {
                entity.ToTable("escolas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Agrup).HasColumnName("agrup");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.AgrupNavigation)
                    .WithMany(p => p.Escolas)
                    .HasForeignKey(d => d.Agrup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_escolas_agrupamentos");
            });

            modelBuilder.Entity<Mensagens>(entity =>
            {
                entity.ToTable("mensagens");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Aluno).HasColumnName("aluno");

                entity.Property(e => e.Datahora)
                    .IsRequired()
                    .HasColumnName("datahora")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Professor).HasColumnName("professor");

                entity.Property(e => e.Texto)
                    .IsRequired()
                    .HasColumnName("texto")
                    .IsUnicode(false);

                entity.HasOne(d => d.AlunoNavigation)
                    .WithMany(p => p.Mensagens)
                    .HasForeignKey(d => d.Aluno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_mensagens_alunos");

                entity.HasOne(d => d.ProfessorNavigation)
                    .WithMany(p => p.Mensagens)
                    .HasForeignKey(d => d.Professor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_mensagens_professores");
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.ToTable("pais");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .IsUnicode(false);

                entity.Property(e => e.Telefone).HasColumnName("telefone");
            });

            modelBuilder.Entity<Professores>(entity =>
            {
                entity.ToTable("professores");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Disciplina)
                    .IsRequired()
                    .HasColumnName("disciplina")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .IsUnicode(false);

                entity.Property(e => e.Escola).HasColumnName("escola");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .IsUnicode(false);

                entity.Property(e => e.Telefone).HasColumnName("telefone");

                entity.HasOne(d => d.EscolaNavigation)
                    .WithMany(p => p.Professores)
                    .HasForeignKey(d => d.Escola)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_professores_escolas");
            });

            modelBuilder.Entity<Sumario>(entity =>
            {
                entity.ToTable("sumario");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnName("data")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Professor).HasColumnName("professor");

                entity.Property(e => e.Sumario1)
                    .IsRequired()
                    .HasColumnName("sumario")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Turma).HasColumnName("turma");

                entity.HasOne(d => d.ProfessorNavigation)
                    .WithMany(p => p.Sumario)
                    .HasForeignKey(d => d.Professor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sumario_professores");

                entity.HasOne(d => d.TurmaNavigation)
                    .WithMany(p => p.Sumario)
                    .HasForeignKey(d => d.Turma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sumario_turmas");
            });

            modelBuilder.Entity<Turmas>(entity =>
            {
                entity.ToTable("turmas");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Escola).HasColumnName("escola");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EscolaNavigation)
                    .WithMany(p => p.Turmas)
                    .HasForeignKey(d => d.Escola)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_turmas_escolas");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
