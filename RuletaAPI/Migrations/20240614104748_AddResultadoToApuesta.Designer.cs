﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RuletaAPI.Data;

#nullable disable

namespace RuletaAPI.Migrations
{
    [DbContext(typeof(RuletaContext))]
    [Migration("20240614104748_AddResultadoToApuesta")]
    partial class AddResultadoToApuesta
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RuletaAPI.Models.Apuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<decimal>("Monto")
                        .HasColumnType("numeric");

                    b.Property<string>("Paridad")
                        .HasColumnType("text");

                    b.Property<decimal>("Resultado")
                        .HasColumnType("numeric");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.Property<string>("Valor")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Apuestas", (string)null);
                });

            modelBuilder.Entity("RuletaAPI.Models.ApuestaTemporal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<decimal>("Monto")
                        .HasColumnType("numeric");

                    b.Property<string>("Paridad")
                        .HasColumnType("text");

                    b.Property<decimal>("Resultado")
                        .HasColumnType("numeric");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.Property<string>("Valor")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ApuestasTemporales", (string)null);
                });

            modelBuilder.Entity("RuletaAPI.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("RuletaAPI.Models.Apuesta", b =>
                {
                    b.HasOne("RuletaAPI.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("RuletaAPI.Models.ApuestaTemporal", b =>
                {
                    b.HasOne("RuletaAPI.Models.Usuario", "Usuario")
                        .WithMany("ApuestasTemporales")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("RuletaAPI.Models.Usuario", b =>
                {
                    b.Navigation("ApuestasTemporales");
                });
#pragma warning restore 612, 618
        }
    }
}
