using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;

namespace Api_Film_P1.Models.EntityFramework
{
    [Table("t_e_film_flm")]
    [Index("flm_titre")]
    public class Film
    {
        [Key]
        [Column("flm_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FilmId { get; set; }

        [Column("flm_titre")]
        [StringLength(100)]
        public string Titre { get; set; } = null!;

        [Column("flm_resume")]
        public string? Resume { get; set; }

        [Column("flm_datesortie")]
        public DateTime DateSortie { get; set; }

        [Column("flm_duree")]
        public decimal Duree { get; set; }

        [Column("flm_genre")]
        public decimal Genre { get; set; }

        [InverseProperty("IdfilmNavigation")]
        public virtual ICollection<Notation> NotesFilm { get; set; } = new List<Notation>();
    }
}
