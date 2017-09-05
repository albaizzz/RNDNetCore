using System;
using Microsoft.Extensions.Configuration;

namespace bal.business
{
    public  class TestBAL2{
        protected IConfiguration Configuration;
        public TestBAL2(IConfiguration config)
        {
            this.Configuration = config;
        }

        public void getConfig()
        {
            var a = Configuration.GetConnectionString("ConnectionStringCS");
        }
    }
}