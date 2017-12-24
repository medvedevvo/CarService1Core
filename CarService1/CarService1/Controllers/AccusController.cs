using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarService1.Models;

namespace CarService1.Controllers
{
    [Produces("application/json")]
    [Route("api/Accus")]
    public class AccusController : Controller
    {
        private Accus accus = Accus.getInstance();
        private DBObjects dbObj = DBObjects.getInstance();

        // GET api/accus
        [HttpGet]
        public IActionResult GetAccus()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccuListWithTime accuListWithTime = new AccuListWithTime(Convert.ToInt32(dbObj.objects_list[1].parameters[1].val), accus.get());

            if (accuListWithTime.accu == null)
            {
                return NotFound();
            }

            return Ok(accuListWithTime);
        }

        // GET: api/accus/5
        [HttpGet("{id}", Name = "GetAccu")]
        public IActionResult GetAccu([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var a = new AccuListWithTime(Convert.ToInt32(dbObj.objects_list[1].parameters[1].val), accus.get(id));
            
            if ((a == null) || (a.accu == null))
            {
                return NotFound();
            }

            return Ok(a);
        }

    }
}