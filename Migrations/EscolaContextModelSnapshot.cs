﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaEscola.Data;

namespace SistemaEscola.Migrations
{
    [DbContext(typeof(EscolaContext))]
    partial class EscolaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("SistemaEscola.Models.Aluno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CPFResponsavel")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Nascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeResponsavel")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("SistemaEscola.Models.Matricula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AlunoId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataMatricula")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TurmaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("TurmaId");

                    b.ToTable("Matriculas");
                });

            modelBuilder.Entity("SistemaEscola.Models.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CPF")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Nascimento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Professores");
                });

            modelBuilder.Entity("SistemaEscola.Models.Turma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProfessorId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Sala")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Turmas");
                });

            modelBuilder.Entity("SistemaEscola.Models.Matricula", b =>
                {
                    b.HasOne("SistemaEscola.Models.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId");

                    b.HasOne("SistemaEscola.Models.Turma", "Turma")
                        .WithMany()
                        .HasForeignKey("TurmaId");
                });

            modelBuilder.Entity("SistemaEscola.Models.Turma", b =>
                {
                    b.HasOne("SistemaEscola.Models.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorId");
                });
#pragma warning restore 612, 618
        }
    }
}
