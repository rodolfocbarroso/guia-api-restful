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
    [Route("produtos")]
    public class ProdutoController : Controller
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Produto>>> Get([FromServices] DataContext context)
        {
            var produtos = await context.Produtos.AsNoTracking().ToListAsync();

            return Ok(produtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Produto>>> GetById(int id, [FromServices] DataContext context)
        {
            var produto = await context.Produtos
                .Include(produto => produto.Categoria)
                .AsNoTracking()
                .FirstOrDefaultAsync(produto => produto.Id == id);

            return Ok(produto);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Produto>> Post([FromBody] Produto model, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await context.Produtos.AddAsync(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar o produto" });
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Produto>> Put(int id, [FromBody] Produto model, [FromServices] DataContext context)
        {
            if (id != model.Id)
                return NotFound(new { message = "Produto não encontrado" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Produto>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Não foi possível atualizar o produto" });
            }
        }
    }
}
