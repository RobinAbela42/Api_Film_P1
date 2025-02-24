using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;

namespace Api_Film_P1.Models.EntityFramework
{
    public partial class FilmRatingsDBContext : DbContext
    {
        public FilmRatingsDBContext()
        {

        }

        public FilmRatingsDBContext(DbContextOptions<FilmRatingsDBContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Notation> Notations { get; set; }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                            .EnableSensitiveDataLogging()
                            .UseNpgsql("Server=localhost;port=5432;Database=TheFOOL; uid=postgres; password=postgres;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.UtilisateurId).HasName("");
                entity.HasIndex(u => u.Mail).IsUnique();
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.FilmId).HasName("flm_id");
                entity.HasIndex(u => u.Titre);
            });

            modelBuilder.Entity<Notation>(entity =>
            {
                entity.HasKey(e => new { e.FilmId, e.UtilisateurId}).HasName("pk_notation");

                entity.HasOne(d => d.FilmNote).WithMany(p => p.NotesFilm)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_notation_film");

                entity.HasOne(d => d.UtilisateurNotant).WithMany(p => p.NotesUtilisateur)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_notation_utilisateur");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
