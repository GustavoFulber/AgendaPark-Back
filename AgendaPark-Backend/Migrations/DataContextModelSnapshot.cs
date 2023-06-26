﻿// <auto-generated />
using System;
using AgendaPark_Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgendaPark_Backend.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AgendaPark_Backend.Models.Agenda", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("aprovado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("atividade")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("dataHoraChegada")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("dataHoraSaida")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("deletado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("empresaCurso")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("instituicaoCurso")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("laboratorioid")
                        .HasColumnType("int");

                    b.Property<string>("observacao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("responsavel")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.HasIndex("laboratorioid");

                    b.ToTable("Agenda");
                });

            modelBuilder.Entity("AgendaPark_Backend.Models.CaracteristicasLab", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("laboratorioid")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("valor")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.HasIndex("laboratorioid");

                    b.ToTable("CaracteristicasLab");
                });

            modelBuilder.Entity("AgendaPark_Backend.Models.Equipamento", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("deletado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("localizacao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("marca")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("modelo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("patrimonio")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("tipo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Equipamento");
                });

            modelBuilder.Entity("AgendaPark_Backend.Models.EquipamentoAgenda", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("agendaid")
                        .HasColumnType("int");

                    b.Property<int>("equipamentoid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("agendaid");

                    b.HasIndex("equipamentoid");

                    b.ToTable("EquipamentoAgenda");
                });

            modelBuilder.Entity("AgendaPark_Backend.Models.Informacoes", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("deletado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("titulo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Informacoes");
                });

            modelBuilder.Entity("AgendaPark_Backend.Models.Laboratorio", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("capacidade")
                        .HasColumnType("int");

                    b.Property<bool>("deletado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("localiza_dentro_Predio")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<float>("m2")
                        .HasColumnType("float");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("predioid")
                        .HasColumnType("int");

                    b.Property<string>("sigla")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.HasIndex("predioid");

                    b.ToTable("Laboratorio");
                });

            modelBuilder.Entity("AgendaPark_Backend.Models.Predio", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("deletado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("endereco")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Predio");
                });

            modelBuilder.Entity("AgendaPark_Backend.Models.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("aprovado")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("deletado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("nivel_acesso")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("numeroAvaliacoes")
                        .HasColumnType("int");

                    b.Property<int>("numeroEstrelas")
                        .HasColumnType("int");

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("AgendaPark_Backend.Models.Agenda", b =>
                {
                    b.HasOne("AgendaPark_Backend.Models.Laboratorio", "laboratorio")
                        .WithMany()
                        .HasForeignKey("laboratorioid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("laboratorio");
                });

            modelBuilder.Entity("AgendaPark_Backend.Models.CaracteristicasLab", b =>
                {
                    b.HasOne("AgendaPark_Backend.Models.Laboratorio", "laboratorio")
                        .WithMany()
                        .HasForeignKey("laboratorioid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("laboratorio");
                });

            modelBuilder.Entity("AgendaPark_Backend.Models.EquipamentoAgenda", b =>
                {
                    b.HasOne("AgendaPark_Backend.Models.Agenda", "agenda")
                        .WithMany()
                        .HasForeignKey("agendaid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgendaPark_Backend.Models.Equipamento", "equipamento")
                        .WithMany()
                        .HasForeignKey("equipamentoid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("agenda");

                    b.Navigation("equipamento");
                });

            modelBuilder.Entity("AgendaPark_Backend.Models.Laboratorio", b =>
                {
                    b.HasOne("AgendaPark_Backend.Models.Predio", "predio")
                        .WithMany()
                        .HasForeignKey("predioid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("predio");
                });
#pragma warning restore 612, 618
        }
    }
}
