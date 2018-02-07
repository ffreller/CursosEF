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

        /// <summary>
        /// Lista todas as áreas cadastradas
        /// </summary>
        /// <returns> lista de áreas</returns>
        /// <response code "200"> Retorna uma lista de área</response>
        /// <response code "400"> Ocorreu um erro</response>
        /// <response code "404"> Url não encontrada</response>
        [HttpGet(Name = "Areas")]
        [ProducesResponseType(typeof(List<Area>),200)]
        [ProducesResponseType(typeof(string),400)]
        public IEnumerable<Area> Listar()
        {
            return contexto.Area.ToList();
        }

        /// <summary>
        /// Lista dados da área requisitada
        /// </summary>
        /// <param Name="idArea"></param>
        /// <returns> área requisitada </returns>
        /// <response code "200"> Retorna uma lista de área</response>
        /// /// <response code "400"> Ocorreu um erro</response>
        /// /// <response code "404"> Url não encontrada</response>
        [HttpGet("{idArea}", Name="AreaAtual")]
        [ProducesResponseType(typeof(Area),200)]
        [ProducesResponseType(typeof(string),400)]
        public Area Listar(int idArea)
        {
            return contexto.Area.Where(x => x.idArea == idArea).FirstOrDefault();
        }

        /// <summary>
        /// Cadastra nova área
        /// </summary>
        /// <param Name="area"></param>
        /// <returns> ok </returns>
        /// <response code "200"> Retorna uma lista de área</response>
        /// /// <response code "400"> Ocorreu um erro</response>
        /// /// <response code "404"> Url não encontrada</response>
        [HttpPost]
        [ProducesResponseType(typeof(Area),200)]
        [ProducesResponseType(typeof(BadRequestObjectResult),400)]
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
        
        /// <summary>
        /// Atualiza a área indicada
        /// </summary>
        /// <param Name="idArea"></param>
        /// <param Name="area"></param>
        /// <returns> ok </returns>
        /// <response code "200"> Retorna uma lista de área</response>
        /// <response code "400"> Ocorreu um erro</response>
        /// <response code "404"> Url não encontrada</response>
        [HttpPut("{idArea}")]
        [ProducesResponseType(typeof(Area),200)]
        [ProducesResponseType(typeof(BadRequestObjectResult),400)]
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

        /// <summary>
        /// Deleta área indicada
        /// </summary>
        /// <param Name="idArea"></param>
        /// <returns> ok </returns>
        /// <response code "200"> Retorna uma lista de área</response>
        /// <response code "400"> Ocorreu um erro</response>
        /// <response code "404"> Url não encontrada</response>
        [HttpDelete("{idArea}")]
        [ProducesResponseType(typeof(List<Area>),200)]
        [ProducesResponseType(typeof(NotFoundObjectResult),400)]
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