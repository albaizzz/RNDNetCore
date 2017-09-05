using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using webapi.business.BAL;
using webapi.common;
using webapi.business.cache;
using webapi.data.DataModel;
using Newtonsoft.Json;
using webapi.common.Logging;
using System.Text;

namespace webapi.api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : BaseController
    { 
        protected Configuration Configuration;
        public ValuesController(IConfiguration configuration, Configuration config):base(configuration)
        {
            this.Configuration = config;
        }
        
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            // var config = Configuration.GetConnectionString("ConnectionStringCS");
            // var configg = Configuration.GetSection("ConnectionStrings:ReservationEntities:ProviderName");
            // var a = Configuration.GetSection("ConnectionStrings:ReservationEntities:ProviderName");
            // ConnectionStrings cs = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            TestBAL2 test = new TestBAL2(Configuration);
            var data = test.GetAlipayData(10053551);
            return Ok(data);
        }

        [HttpPost]
        [Route("SetRedisValue")]
        public IActionResult SetRedisValue([FromBody] AlipayData alipayData)
        {
            var RedisBiz = new RedisBiz(Configuration.AppSettings.RedisUrl, int.Parse(Configuration.AppSettings.LifeTime), Configuration.AppSettings.RedisOn);
            RedisBiz.SetRedis("netcore.redis", JsonConvert.SerializeObject(alipayData));
            return Ok();
        }

        [HttpGet]
        [Route("GetRedisValue")]
        public IActionResult GetRedisValue()
        {
            var RedisBiz = new RedisBiz(Configuration.AppSettings.RedisUrl, int.Parse(Configuration.AppSettings.LifeTime), Configuration.AppSettings.RedisOn);
            var data = RedisBiz.GetRedis("netcore.redis");
            var logger = new Logging("GetRedisValue");
            logger.Info(data);
            return Ok(data);
        }

        [HttpPost]
        [Route("SetQue")]
        public IActionResult SetQue([FromBody] AlipayData alipayData)
        {
            var logger = new Logging("BBM/RequestAccessToken");
            logger.Info(JsonConvert.SerializeObject(alipayData), "input");
            try
            {
                var rabbitMqHost = Configuration.AppSettings.RabbitMqQueueHost;
                var queueName = "netcore.queue";
                var connectionFactory = new RabbitMQ.Client.ConnectionFactory() { HostName = rabbitMqHost };
                using (var connection = connectionFactory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queueName, false, false, false, null);

                        RabbitMQ.Client.IBasicProperties props = channel.CreateBasicProperties();

                        props.ContentType = "text/plain";
                        props.DeliveryMode = 2;

                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(alipayData));

                        channel.BasicPublish(string.Empty, queueName,true, props, body);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }   
            return Ok();
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
