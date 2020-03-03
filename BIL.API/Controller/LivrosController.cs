using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BIL.Data;
using BIL.Data.Entidades;
using BIL.Shared;
using BIL.Logica.Mapper;
using BIL.Logica.Manager.Interface;
using Microsoft.AspNetCore.Authorization;

namespace BIL_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroManager _livroManager;

        public LivrosController(ILivroManager livroManager)
        {
            _livroManager = livroManager;
        }

        // GET: api/Livros
        [HttpGet]
        public async Task<IEnumerable<LivroDto>> GetLivros()
        {
            var livros = await _livroManager.GetLivrosAsync();

            return livros;
        }

        // GET: api/Livros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LivroDto>> GetLivro(int id)
        {
            var livro = await _livroManager.GetLivroAsync(id);

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        // PUT: api/Livros/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, Livro livro)
        {
            /*
            if (id != livro.Id)
            {
                return BadRequest();
            }

            _context.Entry(livro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */
            return NoContent();
        }

        // POST: api/Livros
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(LivroDto livro)
        {
            var livroCriado = await _livroManager.CreateLivroAsync(livro);

            return CreatedAtAction("GetLivro", new { id = livroCriado.Id }, livroCriado);
        }

        // DELETE: api/Livros/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Livro>> DeleteLivro(int id)
        {
            /*
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            */

            var livro = new Livro()
            {
                Id = 1
            };

            return livro;
        }

        private bool LivroExists(int id)
        {
            var livro = _livroManager.GetLivroAsync(id);
            return livro != null;
        }
    }
}
