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
    [Route("api/Init")]
    public class InitController : Controller
    {
        private DBObjects dBObjects = DBObjects.getInstance();

        // PUT: api/init/5
        [HttpPut("{IdCar}")]
        public IActionResult PutId([FromRoute] int IdCar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dBObjects.IdCar = IdCar;

            return NoContent();
        }
    }
}