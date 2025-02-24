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

        [ForeignKey(nameof(FilmId))]
        [InverseProperty(nameof(Film.Notations))]
        public virtual Film IdfilmNavigation { get; set; } = null!;

        [ForeignKey(nameof(UtilisateurId))]
        [InverseProperty(nameof(Utilisateur.Notations))]
        public virtual Utilisateur IdutilisateurNavigation { get; set; } = null!;
    }
}
