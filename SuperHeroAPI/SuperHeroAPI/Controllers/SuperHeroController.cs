using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        [HttpGet]
        /*  ActionResult is a class that represents the result of an action method in an ASP.NET MVC or ASP.NET Core application
         *  An action result is what a controller action returns in response to a browser request. 
         *  They are derived from the abstract class ActionResult, which is the base class for all types of responses.
         * 
         */
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes() {
            return new List<SuperHero> {
                new SuperHero {
                    Name = "Spider Man",
                    firstName = "Peter",
                    lastName = "Parker",
                    place = " New York"
                }
            };
        }
    }
}
