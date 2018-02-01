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
        [HttpGet]
        public IEnumerable<Curso> Listar()
        {
            return contexto.Curso.ToList();
        }

        [HttpGet("{idCurso}")]
        public Curso Listar(int idCurso)
        {
            return contexto.Curso.Include("Pedido").Where(x => x.idCurso == idCurso).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult Cadastro([FromBody] Curso curso)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            contexto.Curso.Add(curso);
            int x = contexto.SaveChanges();
            if (x > 0)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("{idCurso}")]
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
                return Ok();
            else
                return BadRequest();

        }

        [HttpDelete("{idCurso}")]
        public IActionResult Apagar (int idCurso)
        {
            var curso = contexto.Curso.Where(x=>x.idCurso==idCurso).FirstOrDefault();
            if(curso == null)
                return NotFound();

            contexto.Curso.Remove(curso);
            int rs = contexto.SaveChanges();
            if(rs > 0)
                return Ok();
            else
                return BadRequest();
        }
    }
}