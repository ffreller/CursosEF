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

        /// <summary>
        /// Lista todas as turmas cadastradas
        /// </summary>
        /// <returns> lista de turmas</returns>
        /// <responde code "200"> Retorna uma lista de turma</response>
        /// /// <responde code "400"> Ocorreu um erro</response>
        [HttpGet(Name = "Turmas")]
        [ProducesResponseType(typeof(List<Turma>),200)]
        [ProducesResponseType(typeof(string),400)]
        public IEnumerable<Turma> Listar()
        {
            return contexto.Turma.ToList();
        }

         /// <summary>
        /// Lista dados da turma requisitada
        /// </summary>
        /// <returns> turma requisitada </returns>
        /// <responde code "200"> Retorna uma lista de turma</response>
        /// /// <responde code "400"> Ocorreu um erro</response>
        [HttpGet("{idTurma}", Name = "TurmaAtual")]
        [ProducesResponseType(typeof(Turma),200)]
        [ProducesResponseType(typeof(string),400)]
        public Turma Listar(int idTurma)
        {
            return contexto.Turma.Include("Pedido").Where(x => x.idTurma == idTurma).FirstOrDefault();
        }

        /// <summary>
        /// Cadastra nova turma
        /// </summary>
        /// <returns> Lista de turma </returns>
        /// <responde code "200"> Retorna uma lista de turma</response>
        /// /// <responde code "400"> Ocorreu um erro</response>
        [HttpPost]
        [ProducesResponseType(typeof(Turma),200)]
        [ProducesResponseType(typeof(BadRequestObjectResult),400)]
        public IActionResult Cadastro([FromBody] Turma turma)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            contexto.Turma.Add(turma);
            int x = contexto.SaveChanges();
            if (x > 0)
                return CreatedAtRoute("TurmaAtual", new{idTurma = turma.idTurma}, turma);
            else
                return BadRequest();
        }

        /// <summary>
        /// Atualiza a turma indicada
        /// </summary>
        /// <returns> ok </returns>
        /// <responde code "200"> Retorna uma lista de turma</response>
        /// /// <responde code "400"> Ocorreu um erro</response>
        [HttpPut("{idTurma}")]
        [ProducesResponseType(typeof(Turma),200)]
        [ProducesResponseType(typeof(BadRequestObjectResult),400)]
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
            tur.diasemana = turma.diasemana;

            contexto.Turma.Update(tur);
            int rs = contexto.SaveChanges();

            if(rs > 0)
                return CreatedAtRoute("TurmaAtual", new{idTurma = turma.idTurma}, turma);
            else
                return BadRequest();

        }

         /// <summary>
        /// Deleta turma indicada
        /// </summary>
        /// <returns> ok </returns>
        /// <responde code "200"> Retorna uma lista de turma</response>
        /// /// <responde code "400"> Ocorreu um erro</response>
        [HttpDelete("{idTurma}")]
        [ProducesResponseType(typeof(List<Turma>),200)]
        [ProducesResponseType(typeof(NotFoundObjectResult),400)]
        public IActionResult Apagar (int idTurma)
        {
            var turma = contexto.Turma.Where(x=>x.idTurma==idTurma).FirstOrDefault();
            if(turma == null)
                return NotFound();

            contexto.Turma.Remove(turma);
            int rs = contexto.SaveChanges();
            if(rs > 0)
                return Redirect("Turmas");
            else
                return BadRequest();
        }
    }
}