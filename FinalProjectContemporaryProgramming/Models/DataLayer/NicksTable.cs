using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FinalProjectContemporaryProgramming.Models.DataLayer
{
    public partial class NicksTable
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
        //[Required]
        [Column("FavoriteTA")]
        [StringLength(50)]
        public string FavoriteTa { get; set; }
        //[Required]
        [StringLength(50)]
        public string FavoriteTeacher { get; set; }
        //[Required]
        [StringLength(50)]
        public string FavoriteClass { get; set; }
        
        public bool IsALiar { get; set; }
    }
}
