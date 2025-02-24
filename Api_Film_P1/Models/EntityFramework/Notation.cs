using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Api_Film_P1.Models.EntityFramework
{
    [Table("t_j_notation_not")]
    public class Notation
    {
        [Key]
        [Column("flm_id")]
        public int UtilisateurId { get; set; }

        [Key]
        [Column("utl_id")]
        public int FilmId { get; set; }

        [Column("not_note")]
        public int Note { get; set; }

        [ForeignKey("fk_not_flm")]
        [InverseProperty(nameof(Film.NotesFilm))]
        public virtual Film FilmNote { get; set; } = null!;

        [ForeignKey("fk_not_utl")]
        [InverseProperty(nameof(Utilisateur.NotesUtilisateur))]
        public virtual Utilisateur UtilisateurNotant { get; set; } = null!;
    }
}
