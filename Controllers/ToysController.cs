using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OMI_AWS_v2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToysController : ControllerBase
    {
        // GET: api/<ToysController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Megatron", "HeMan", "Pokebola" };
        }

        // GET api/<ToysController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            if(id == 1)
                return "Megatron";
            else if (id == 2)
                return "HeMan";
            else if (id == 3)
                return "Pokebola";
            else
                return "Muñeca System";


        }

        // POST api/<ToysController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ToysController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ToysController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
