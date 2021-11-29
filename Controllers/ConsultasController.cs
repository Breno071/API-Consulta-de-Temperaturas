using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteTecnico_.NET.Data;
using TesteTecnico_.NET.Models;

namespace TesteTecnico_.NET.Controllers
{
  [Route("api/v1/[controller]")]
  [ApiController]
  public class ConsultasController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public ConsultasController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: api/v1/Consultas/curitiba
    [HttpGet("curitiba")]
    public async Task<ActionResult<Main>> GetCuritibaAsync()
    {
      return await _context.Temps
          .OrderByDescending(a => a.id)
          .Where(city => city.name == "curitiba")
          .FirstOrDefaultAsync();
    }

    // GET: api/v1/Consultas/porto_alegre
    [HttpGet("porto_alegre")]
    public async Task<ActionResult<Main>> GetPorto_AlegreAsync()
    {
      return await _context.Temps
          .OrderByDescending(a => a.id)
          .Where(city => city.name == "porto alegre")
          .FirstOrDefaultAsync();
    }

    // GET: api/v1/Consultas/florianopolis
    [HttpGet("florianopolis")]
    public async Task<ActionResult<Main>> GetFlorianopolisAsync()
    {
      return await _context.Temps
          .OrderByDescending(a => a.id)
          .Where(city => city.name == "florianópolis")
          .FirstOrDefaultAsync();
    }

    // GET: api/v1/Consultas/cidade
    [HttpGet("cidade")]
    public async Task<ActionResult<Main>> GetCidadeFromNameAsync(string cidade)
    {
      if (cidade == null)
      {
        return BadRequest("Null");
      }
      var consulta = await _context.Temps
      .OrderByDescending(a => a.id)
      .Where(city => city.name.Contains(cidade.ToLower()))
      .FirstOrDefaultAsync();
      return consulta;
    }

    // GET: api/v1/Consultas/cidades_periodo
    [HttpGet("cidades_periodo")]
    public async Task<ActionResult<IEnumerable<Main>>> GetCidadesFromDateAsync(DateTime dataInicio, DateTime dataFim)
    {
      var consulta = _context.Temps
          .Where(city => city.date >= dataInicio.Date && city.date <= dataFim.Date)
          .OrderBy(city => city.date);
      if (!consulta.Any())
      {
        return NotFound("Não foram encontradas cidades nesse período");
      }
      return await consulta.ToListAsync();
    }

    // GET: api/v1/Consultas/cidades_periodo_name
    [HttpGet("cidades_periodo_name")]
    public async Task<ActionResult<IEnumerable<Main>>> GetCidadesFromDateAndNameAsync(string cidade, DateTime dataInicio, DateTime dataFim)
    {
      var consulta = _context.Temps
          .Where(city =>
          city.date >= dataInicio.Date && city.date <= dataFim.Date && city.name.Contains(cidade.ToLower()))
          .OrderBy(city => city.date);
      if (!consulta.Any())
      {
        return NotFound("Não foram encontradas cidades nesse período");
      }
      return await consulta.ToListAsync();
    }

    // GET: api/v1/Consultas/cidades
    [HttpGet("cidades")]
    public async Task<ActionResult<IEnumerable<Main>>> GetTodasCidadesAsync()
    {
      return await _context.Temps.ToListAsync();
    }
  }
}
