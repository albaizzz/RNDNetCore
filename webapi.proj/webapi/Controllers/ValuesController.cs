﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using bal.business;

namespace webapi.proj.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : BaseController
    { 
        public ValuesController(IConfiguration configuration):base(configuration)
        {
            
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
            // var data = dalTest.GetAlipayOrderData(10053551);
            TestBAL TestBAL = new TestBAL();
            var data = TestBAL.GetAlipayData(10053551);
            // return data;
            
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
