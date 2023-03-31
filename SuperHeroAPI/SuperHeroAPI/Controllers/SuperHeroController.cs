using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly SuperHeroDbContext _context;

        /*
         * We need to inject in our controller the database connection so the controller can have access
         * to the DB, and therefore make the necessary changes in in during a request or response
         */
        public SuperHeroController(SuperHeroDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        /*  ActionResult is a class that represents the result of an action method in an ASP.NET MVC or ASP.NET Core application
         *  An action result is what a controller action returns in response to a browser request. 
         *  They are derived from the abstract class ActionResult, which is the base class for all types of responses.
         * 
         */
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes() {
            /*  The code commented, we're hardcoding the information. Instead, we want to access the data from the database
             *  We're going to return Ok in case the request is successfull (code 200), and we are going to use our
             *  _context (SuperHeroDbContext) to access it. We're going to that async and convert it to List
             * 
             */
            return Ok(await _context.SuperHeroes.ToListAsync());
            
            //return new List<SuperHero> {
            //    new SuperHero {
            //        Name = "Spider Man",
            //        firstName = "Peter",
            //        lastName = "Parker",
            //        place = " New York"
            //    }
            //};
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero hero) { 
            /*
             * The add method is a built-in method from Entity framework
             * The SaveChangesAsync is a built-int method from DbContext
             */
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            //After adding the new hero, we return the new, updated list
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        //Put updates the whole object (hero). Patch updates partially one property of object (for instance, just the name of the hero)
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero) {
            //check if hero is available
            var dbHero = await _context.SuperHeroes.FindAsync(hero.Id);
            if (dbHero == null) {
                return BadRequest("This hero doesn't exist in the Database yet");
            }
            //Else, update the values. Since it is a Put request, we have to update everything

            dbHero.Name = hero.Name;
            dbHero.firstName = hero.firstName;
            dbHero.lastName = hero.lastName;
            dbHero.place= hero.place;

            //Finally, we need to save our changes
            await _context.SaveChangesAsync();
            //And return the new, updated list
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        //We need to pass one parameter in the delete request, which is the Id of the hero to be deleted
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(SuperHero hero) {
            //check if hero is available
            var dbHero = await _context.SuperHeroes.FindAsync(hero.Id);
            if (dbHero == null)
            {
                return BadRequest("This hero doesn't exist in the Database and therefore cannot be deleted");
            }
            //Else, delete it
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

    }
}
