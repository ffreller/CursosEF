using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CursosEF.Models
{
    public class Area
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idArea { get; set; }

        [Required(ErrorMessage="Campo nome não pode ficar nulo")]
        [StringLength(70,MinimumLength=2)]
        public string nomeArea { get; set; }

        public ICollection<Curso> curso { get; set; }

    }
}