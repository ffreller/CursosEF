using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CursosEF.Models;
using CursosEF.Contextos;
using Microsoft.EntityFrameworkCore;

namespace CursosEF.Controllers
{
    [Route("api/[controller]")]
    public class AreaController:Controller
    {
        Area area = new Area();
        readonly CursosContexto contexto;

        public AreaController(CursosContexto contexto)
        {
            this.contexto = contexto;
        }
        [HttpGet]
        public IEnumerable<Area> Listar()
        {
            return contexto.Area.ToList();
        }

        [HttpGet("{idArea}")]
        public Area Listar(int idArea)
        {
            return contexto.Area.Where(x => x.idArea == idArea).FirstOrDefault();
        }

        [HttpPost]
        public IActionResult Cadastro([FromBody] Area area)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            contexto.Area.Add(area);
            int x = contexto.SaveChanges();
            if (x > 0)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("{idArea}")]
        public IActionResult Atualizar (int idArea, [FromBody] Area area)
        {
            if (area == null || area.idArea!=idArea){
                return BadRequest();
            }
            var are = contexto.Area.FirstOrDefault();
            if (are == null)
                return NotFound();
            
            are.idArea = area.idArea;
            are.nomeArea = area.nomeArea;
            

            contexto.Area.Update(are);
            int rs = contexto.SaveChanges();

            if(rs > 0)
                return Ok();
            else
                return BadRequest();

        }

        [HttpDelete("{idArea}")]
        public IActionResult Apagar (int idArea)
        {
            var area = contexto.Area.Where(x=>x.idArea==idArea).FirstOrDefault();
            if(area == null)
                return NotFound();

            contexto.Area.Remove(area);
            int rs = contexto.SaveChanges();
            if(rs > 0)
                return Ok();
            else
                return BadRequest();
        }
    }
}