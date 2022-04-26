using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FinalProjectContemporaryProgramming.Models.DataLayer
{
    public partial class JohnTable
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string FavoriteSport { get; set; }
        [StringLength(50)]
        public string FavoriteVideoGame { get; set; }
        [Column("FavoriteTVShow")]
        [StringLength(50)]
        public string FavoriteTvshow { get; set; }
    }
}
