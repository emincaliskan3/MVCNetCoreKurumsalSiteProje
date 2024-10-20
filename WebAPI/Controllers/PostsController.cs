﻿using Data;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebAPI.Araclar;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PostsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<PostsController>
        [HttpGet]
        public async Task<IEnumerable<Post>> GetAsync()
        {

            return await _context.Posts.ToListAsync();
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetAsync(int id)
        {

            var kayit = await _context.Posts.FindAsync(id);
            //  var kayit = await _context.Posts.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == id); // include yapınca json hatası veriyor!
            if (kayit == null)
            {
                return NotFound();
            }
            return kayit;
        }

        // POST api/<PostsController>
        [HttpPost]
        public async Task<ActionResult<Post>> PostAsync([FromBody] Post value)
        {
            //value.Image = await FileHelper.FileLoaderAsync(Image);
            await _context.Posts.AddAsync(value);
            await _context.SaveChangesAsync();
            return Ok(value); // ok metodu işlemin başarılı olduğu bilgisini döndürür
        }

        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> Put(int id, [FromBody] Post value)//, IFormFile? Image
        {
            //if (Image is not null)
            //{
            //    value.Image = await FileHelper.FileLoaderAsync(Image);
            //}
            _context.Posts.Update(value);
            await _context.SaveChangesAsync();
            return Ok(value);
        }

        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var kayit = await _context.Posts.FindAsync(id);
            if (kayit == null)
            {
                return NotFound();
            }
            _context.Posts.Remove(kayit);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
