﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Senai.MaisVagas.WebApi.Domains;

namespace Senai.MaisVagas.WebApi.Contexts
{
    public partial class MaisVagasContext : DbContext
    {
        public MaisVagasContext()
        {
        }

        public MaisVagasContext(DbContextOptions<MaisVagasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Candidato> Candidato { get; set; }
        public virtual DbSet<Contrato> Contrato { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Inscricao> Inscricao { get; set; }
        public virtual DbSet<Situacao> Situacao { get; set; }
        public virtual DbSet<TipoContrato> TipoContrato { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vaga> Vaga { get; set; }
        public virtual DbSet<VagasFavoritas> VagasFavoritas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-RH355KD; Initial Catalog=MaisVagas; Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.HasKey(e => e.IdAdministrador)
                    .HasName("PK__Administ__2B3E34A88E28A2D7");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Administrador)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Administr__IdUsu__45F365D3");
            });

            modelBuilder.Entity<Candidato>(entity =>
            {
                entity.HasKey(e => e.IdCandidato)
                    .HasName("PK__Candidat__D5598905EA8CDF0A");

                entity.HasIndex(e => e.Cpf)
                    .HasName("UQ__Candidat__C1F897313A722F18")
                    .IsUnique();

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Curriculo)
                    .HasMaxLength(110)
                    .IsUnicode(false);

                entity.Property(e => e.DataNascimento).HasColumnType("date");

                entity.Property(e => e.Matricula)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdCursoNavigation)
                    .WithMany(p => p.Candidato)
                    .HasForeignKey(d => d.IdCurso)
                    .HasConstraintName("FK__Candidato__IdCur__36B12243");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Candidato)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Candidato__IdUsu__37A5467C");
            });

            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.HasKey(e => e.IdContrato)
                    .HasName("PK__Contrato__8569F05AA6452D4F");

                entity.Property(e => e.DataInicio).HasColumnType("date");

                entity.Property(e => e.DataTermino).HasColumnType("date");

                entity.Property(e => e.DescriçaoEstagio)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DescriçãoCancelamento)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DiasContrato)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsavelEstagio)
                    .HasMaxLength(110)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCandidatoNavigation)
                    .WithMany(p => p.Contrato)
                    .HasForeignKey(d => d.IdCandidato)
                    .HasConstraintName("FK__Contrato__IdCand__3F466844");

                entity.HasOne(d => d.IdSituacaoNavigation)
                    .WithMany(p => p.Contrato)
                    .HasForeignKey(d => d.IdSituacao)
                    .HasConstraintName("FK__Contrato__IdSitu__3D5E1FD2");

                entity.HasOne(d => d.IdTipoContratoNavigation)
                    .WithMany(p => p.Contrato)
                    .HasForeignKey(d => d.IdTipoContrato)
                    .HasConstraintName("FK__Contrato__IdTipo__3C69FB99");

                entity.HasOne(d => d.IdVagaNavigation)
                    .WithMany(p => p.Contrato)
                    .HasForeignKey(d => d.IdVaga)
                    .HasConstraintName("FK__Contrato__IdVaga__3E52440B");
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.IdCurso)
                    .HasName("PK__Curso__085F27D617A731D8");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Termo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Turno)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa)
                    .HasName("PK__Empresa__5EF4033EA681B96B");

                entity.HasIndex(e => e.Cnpj)
                    .HasName("UQ__Empresa__AA57D6B48B589124")
                    .IsUnique();

                entity.Property(e => e.Cnae)
                    .IsRequired()
                    .HasColumnName("CNAE")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("CNPJ")
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.ImagemCarimboAssinaturaDoResponsavel)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ImagemCarimboCnpj)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.NomeParaContato)
                    .IsRequired()
                    .HasMaxLength(110)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroEmpregados)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Empresa)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Empresa__IdUsuar__2B3F6F97");
            });

            modelBuilder.Entity<Inscricao>(entity =>
            {
                entity.HasKey(e => e.IdInscricao)
                    .HasName("PK__Inscrica__6209444BD1B807E4");

                entity.HasOne(d => d.IdCandidatoNavigation)
                    .WithMany(p => p.Inscricao)
                    .HasForeignKey(d => d.IdCandidato)
                    .HasConstraintName("FK__Inscricao__IdCan__4316F928");

                entity.HasOne(d => d.IdVagaNavigation)
                    .WithMany(p => p.Inscricao)
                    .HasForeignKey(d => d.IdVaga)
                    .HasConstraintName("FK__Inscricao__IdVag__4222D4EF");
            });

            modelBuilder.Entity<Situacao>(entity =>
            {
                entity.HasKey(e => e.IdSituacao)
                    .HasName("PK__Situacao__810BCE3A39678762");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoContrato>(entity =>
            {
                entity.HasKey(e => e.IdTipoContrato)
                    .HasName("PK__TipoCont__F46C49C281EF6512");

                entity.Property(e => e.Nome)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PK__TipoUsua__CA04062B123C813F");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF972D7AD4E5");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Usuario__A9D105340D6798FE")
                    .IsUnique();

                entity.Property(e => e.Bairro)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Cep)
                    .HasColumnName("CEP")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Cidade)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Foto)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(117)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__Usuario__IdTipoU__276EDEB3");
            });

            modelBuilder.Entity<Vaga>(entity =>
            {
                entity.HasKey(e => e.IdVaga)
                    .HasName("PK__Vaga__A848DC3E916817AF");

                entity.Property(e => e.Beneficios)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DescricaoVaga)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HardSkills)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Jornada)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LogoEmpresa)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NivelExperiencia)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.NomeVaga)
                    .IsRequired()
                    .HasMaxLength(110)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroVagaDisponiveis)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.QualificacaoProfissional)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Setor)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SoftSkills)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Vaga)
                    .HasForeignKey(d => d.IdEmpresa)
                    .HasConstraintName("FK__Vaga__IdEmpresa__30F848ED");

                entity.HasOne(d => d.IdTipoContratoNavigation)
                    .WithMany(p => p.Vaga)
                    .HasForeignKey(d => d.IdTipoContrato)
                    .HasConstraintName("FK__Vaga__IdTipoCont__300424B4");
            });

            modelBuilder.Entity<VagasFavoritas>(entity =>
            {
                entity.HasKey(e => e.IdVagasFavoritas)
                    .HasName("PK__VagasFav__33522DA1B3190670");

                entity.HasOne(d => d.IdCandidatoNavigation)
                    .WithMany(p => p.VagasFavoritas)
                    .HasForeignKey(d => d.IdCandidato)
                    .HasConstraintName("FK__VagasFavo__IdCan__48CFD27E");

                entity.HasOne(d => d.IdVagaNavigation)
                    .WithMany(p => p.VagasFavoritas)
                    .HasForeignKey(d => d.IdVaga)
                    .HasConstraintName("FK__VagasFavo__IdVag__49C3F6B7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
