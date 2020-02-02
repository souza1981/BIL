using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BIL.Data;
using BIL.Data.Entidades;

namespace BIL_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorasController : ControllerBase
    {
        private readonly BILContext _context;

        public EditorasController(BILContext context)
        {
            _context = context;
        }

        // GET: api/Editoras
        [HttpGet]
        public async Task<IEnumerable<Editora>> GetEditoras()
        {
            var editorasDB = await _context.Editoras.ToListAsync();
            var editorasDTO = editorasDB.Select(e => new Editora()
            {
                Id = e.Id,
                Email = e.Email,
                Livros = e.Livros,
                Nome = e.Nome
            });

            return editorasDTO;
        }

        // GET: api/Editoras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Editora>> GetEditora(int id)
        {
            var editora = await _context.Editoras.FindAsync(id);

            if (editora == null)
            {
                return NotFound();
            }

            return editora;
        }

        // PUT: api/Editoras/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEditora(int id, Editora editora)
        {
            if (id != editora.Id)
            {
                return BadRequest();
            }

            _context.Entry(editora).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EditoraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Editoras
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Editora>> PostEditora(Editora editora)
        {
            _context.Editoras.Add(editora);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEditora", new { id = editora.Id }, editora);
        }

        // DELETE: api/Editoras/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Editora>> DeleteEditora(int id)
        {
            var editora = await _context.Editoras.FindAsync(id);
            if (editora == null)
            {
                return NotFound();
            }

            _context.Editoras.Remove(editora);
            await _context.SaveChangesAsync();

            return editora;
        }

        private bool EditoraExists(int id)
        {
            return _context.Editoras.Any(e => e.Id == id);
        }
    }
}
