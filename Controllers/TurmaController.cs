using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CursosEF.Models;
using CursosEF.Contextos;
using Microsoft.EntityFrameworkCore;

namespace TurmasEF.Controllers
{
    [Route("api/[controller]")]
    public class TurmaController:Controller
    {
        Turma turma = new Turma();
        readonly CursosContexto contexto;

        public TurmaController(CursosContexto contexto)
        {
            this.contexto = contexto;
        }
        [HttpGet]
        public IEnumerable<Turma> Listar()
        {
            return contexto.Turma.ToList();
        }

        [HttpGet("{idTurma}")]
        public Turma Listar(int idTurma)
        {
            return contexto.Turma.Include("Pedido").Where(x => x.idTurma == idTurma).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult Cadastro([FromBody] Turma turma)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            contexto.Turma.Add(turma);
            int x = contexto.SaveChanges();
            if (x > 0)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("{idTurma}")]
        public IActionResult Atualizar (int idTurma, [FromBody] Turma turma)
        {
            if (turma == null || turma.idTurma!=idTurma){
                return BadRequest();
            }
            var tur = contexto.Turma.FirstOrDefault();
            if (tur == null)
                return NotFound();
            
            tur.idTurma = turma.idTurma;
            tur.idCurso = turma.idCurso;
            tur.datain = turma.datain;
            tur.datafim=turma.datafim;
            tur.horarioin = turma.horarioin;
            tur.horariofim = turma.horariofim;
            tur.diasemana = tur.diasemana;

            contexto.Turma.Update(tur);
            int rs = contexto.SaveChanges();

            if(rs > 0)
                return Ok();
            else
                return BadRequest();

        }

        [HttpDelete("{idTurma}")]
        public IActionResult Apagar (int idTurma)
        {
            var turma = contexto.Turma.Where(x=>x.idTurma==idTurma).FirstOrDefault();
            if(turma == null)
                return NotFound();

            contexto.Turma.Remove(turma);
            int rs = contexto.SaveChanges();
            if(rs > 0)
                return Ok();
            else
                return BadRequest();
        }
    }
}