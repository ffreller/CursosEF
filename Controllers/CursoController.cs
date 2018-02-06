using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CursosEF.Models;
using CursosEF.Contextos;
using Microsoft.EntityFrameworkCore;

namespace CursosEF.Controllers
{
    [Route("api/[controller]")]
    public class CursoController:Controller
    {
        Curso curso = new Curso();
        readonly CursosContexto contexto;

        public CursoController(CursosContexto contexto)
        {
            this.contexto = contexto;
        }

        /// <summary>
        /// Lista todas os cursos cadastrados
        /// </summary>
        /// <returns> lista de cursos</returns>
        /// <responde code "200"> Retorna uma lista de curso</response>
        /// /// <responde code "400"> Ocorreu um erro</response>
        [HttpGet(Name = "Cursos")]
        [ProducesResponseType(typeof(List<Curso>),200)]
        [ProducesResponseType(typeof(string),400)]
        public IEnumerable<Curso> Listar()
        {
            return contexto.Curso.ToList();
        }

        /// <summary>
        /// Lista dados do curso requisitado
        /// </summary>
        /// <returns> curso requisitado </returns>
        /// <responde code "200"> Retorna uma lista de curso</response>
        /// /// <responde code "400"> Ocorreu um erro</response>
        [HttpGet("{idCurso}", Name = "CursoAtual")]
        [ProducesResponseType(typeof(Curso),200)]
        [ProducesResponseType(typeof(string),400)]
        public Curso Listar(int idCurso)
        {
           return contexto.Curso.Where(x => x.idCurso == idCurso).FirstOrDefault();
        }

        /// <summary>
        /// Cadastra novo curso
        /// </summary>
        /// <returns> ok </returns>
        /// <responde code "200"> Retorna uma lista de curso</response>
        /// /// <responde code "400"> Ocorreu um erro</response>
        [HttpPost]
        [ProducesResponseType(typeof(Curso),200)]
        [ProducesResponseType(typeof(BadRequestObjectResult),400)]
        public IActionResult Cadastro([FromBody] Curso curso)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            contexto.Curso.Add(curso);
            int x = contexto.SaveChanges();
            if (x > 0)
                return CreatedAtRoute("CursoAtual", new{idTurma = curso.idCurso}, curso);
            else
                return BadRequest();
        }

        /// <summary>
        /// Atualiza o curso indicada
        /// </summary>
        /// <returns> ok </returns>
        /// <responde code "200"> Retorna uma lista de curso</response>
        /// /// <responde code "400"> Ocorreu um erro</response>
        [HttpPut("{idCurso}")]
        [ProducesResponseType(typeof(Curso),200)]
        [ProducesResponseType(typeof(BadRequestObjectResult),400)]
        public IActionResult Atualizar (int idCurso, [FromBody] Curso curso)
        {
            if (curso == null || curso.idCurso!=idCurso){
                return BadRequest();
            }
            var cur = contexto.Curso.FirstOrDefault();
            if (cur == null)
                return NotFound();
            
            cur.idCurso = curso.idCurso;
            cur.nomeCurso = curso.nomeCurso;
            cur.idArea = curso.idArea;
            cur.area = curso.area;

            contexto.Curso.Update(cur);
            int rs = contexto.SaveChanges();

            if(rs > 0)
                 return CreatedAtRoute("CursoAtual", new{idTurma = curso.idCurso}, curso);
            else
                return BadRequest();

        }

        /// <summary>
        /// Deleta curso indicado
        /// </summary>
        /// <returns> ok </returns>
        /// <responde code "200"> Retorna uma lista de curso</response>
        /// /// <responde code "400"> Ocorreu um erro</response>
        [HttpDelete("{idCurso}")]
        [ProducesResponseType(typeof(List<Curso>),200)]
        [ProducesResponseType(typeof(NotFoundObjectResult),400)]
        public IActionResult Apagar (int idCurso)
        {
            var curso = contexto.Curso.Where(x=>x.idCurso==idCurso).FirstOrDefault();
            if(curso == null)
                return NotFound();

            contexto.Curso.Remove(curso);
            int rs = contexto.SaveChanges();
            if(rs > 0)
                return Redirect("Cursos");
            else
                return BadRequest();
        }
    }
}