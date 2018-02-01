using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CursosEF.Models
{
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCurso { get; set; }

        [Required(ErrorMessage="Campo nome não pode ficar nulo")]
        [StringLength(70,MinimumLength=2)]
        public string nomeCurso { get; set; }

        [Required(ErrorMessage="Campo idArea não pode ficar nulo")]
        public int idArea { get; set; }

        public ICollection<Turma> turma { get; set; }

        public Area area { get; set; }

        

    }
}