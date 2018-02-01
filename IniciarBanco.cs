using System;
using System.Linq;
using CursosEF.Contextos;
using CursosEF.Models;

namespace CursosEF
{
    public class IniciarBanco
    {
        public static void Iniciar(CursosContexto contexto)
        {
            contexto.Database.EnsureCreated();
            if(contexto.Area.Any())
            {
                return;
            }
        
        var area = new Area()
            {nomeArea="Administração Geral"};
        contexto.Area.Add(area);
 
        var curso = new Curso()
            {idArea = area.idArea, nomeCurso="Auxiliar Administrativo"};
        contexto.Curso.Add(curso);

        var turma = new Turma()
            {idCurso=curso.idCurso, datain=Convert.ToDateTime("2018/01/08"), datafim=Convert.ToDateTime("2018/05/11"), horarioin=Convert.ToDateTime("19:00"), horariofim=Convert.ToDateTime("22:30"), diasemana="2a, 4a e 6a"};
        contexto.Turma.Add(turma);
        
        contexto.SaveChanges();
        }
    }
}