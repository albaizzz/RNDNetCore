using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using webapi.data;

namespace webapi.proj.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    { 
        IConfiguration Configuration;
        public ValuesController(IConfiguration iconfiguration)
        {
            Configuration = iconfiguration;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var config = Configuration.GetConnectionString("ConnectionStringCS");
            var configg = Configuration.GetSection("ConnectionStrings:ReservationEntities:ProviderName");
            var a = Configuration.GetSection("ConnectionStrings:ReservationEntities:ProviderName");

            // Class1 cls1 = new Class1();
            // cls1.cls1();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
