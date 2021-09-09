using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpellApplication.Models;
using SpellApplication.Services;

namespace SpellApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        // GET: api/Text
        [HttpGet]
        public JsonResult Get([FromQuery(Name = "space")] string space, [FromQuery(Name = "variable")] string variable)
        {
            if (String.IsNullOrEmpty(space))
            {
                return new JsonResult("");
            }

            var result = new TextLoaderService().LoadText(space, variable);

            return new JsonResult(new Response() { Space = result[0], Gadget = result[1], State = result[2], Lang = result[3], Text = result[4], Variable = variable });
        }
    }
}
