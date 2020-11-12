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

    }
}
