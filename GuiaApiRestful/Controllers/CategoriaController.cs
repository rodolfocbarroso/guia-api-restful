using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuiaApiRestful.Data;
using GuiaApiRestful.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuiaApiRestful.Controllers
{
    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] DataContext context)
        {
            var categorias = await context.Categorias.AsNoTracking().ToListAsync();

            return categorias;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> GetById( int id, [FromServices] DataContext context)
        {
            var categoria = await context.Categorias.AsNoTracking().FirstOrDefaultAsync( categoria => categoria.Id == id);
            
            return categoria;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> Post([FromBody]Categoria model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await context.Categorias.AddAsync(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new {message = "Não foi possível criar a categoria"});
            }

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> Put( int id, [FromBody] Categoria model, [FromServices] DataContext context)
        {
            if (id != model.Id)
                return NotFound(new { message = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
               context.Entry<Categoria>(model).State = EntityState.Modified;
               await context.SaveChangesAsync();

               return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new {message = "Não foi possível atualizar a categoria" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Categoria>>> Delete()
        {
            return Ok();
        }
    }
}
