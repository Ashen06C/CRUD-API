using CRUD_API.Data;
using CRUD_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHerosController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHerosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeros()
        {
            var heros = await _context.SuperHeros.ToListAsync();
            return Ok(heros);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<SuperHero>> GetHeroById(int id)
        {
            var hero = await _context.SuperHeros.FirstOrDefaultAsync(x => x.Id == id);
            if (hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            await _context.SuperHeros.AddAsync(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(int id, SuperHero hero)
        {
            var heroToUpdate = await _context.SuperHeros.FirstOrDefaultAsync(x => x.Id == id);
            if (heroToUpdate == null)
            {
                return NotFound();
            }
            heroToUpdate.Name = hero.Name;
            heroToUpdate.FName = hero.FName;
            heroToUpdate.LName = hero.LName;
            heroToUpdate.City = hero.City;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var heroToDelete = await _context.SuperHeros.FirstOrDefaultAsync(x => x.Id == id);
            if (heroToDelete == null)
            {
                return NotFound();
            }
            _context.SuperHeros.Remove(heroToDelete);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
    }
}
