using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FinalProjectContemporaryProgramming.Models.DataLayer
{
    public partial class MattTable
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
        public string FavoriteBreakfeast { get; set; }
        [StringLength(50)]
        public string FavoriteDinner { get; set; }
        [StringLength(50)]
        public string FavoriteDessert { get; set; }
    }
}
