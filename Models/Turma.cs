using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CursosEF.Models
{
    public class Turma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idTurma { get; set; }
        [Required(ErrorMessage="Campo idCurso não pode ficar nulo")]
        public int idCurso { get; set; }
        [Required(ErrorMessage="Campo datain não pode ficar nulo")]
        public DateTime datain { get; set; }
        [Required(ErrorMessage="Campo datafim não pode ficar nulo")]
        public DateTime datafim { get; set; }
         [Required(ErrorMessage="Campo horarioin não pode ficar nulo")]
        public DateTime horarioin { get; set; }
        [Required(ErrorMessage="Campo horariofim não pode ficar nulo")]
        public DateTime horariofim { get; set; }    

        public string diasemana { get; set; }

        public Curso curso { get; set; }


    }
}