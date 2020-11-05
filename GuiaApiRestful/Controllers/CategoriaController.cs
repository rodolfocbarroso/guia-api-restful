using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuiaApiRestful.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuiaApiRestful.Controllers
{
    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            
            return new List<Categoria>();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> GetById( int id)
        {
            return new Categoria();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> Post([FromBody]Categoria model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(model);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> Put( int id, [FromBody] Categoria model)
        {
            if (id != model.Id)
                return NotFound(new { message = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Categoria>>> Delete()
        {
            return Ok();
        }
    }
}
