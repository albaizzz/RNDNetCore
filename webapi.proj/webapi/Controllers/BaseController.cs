using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using bal.business;


namespace webapi.proj.Controllers
{
      public class BaseController : Controller
      {
        public IConfiguration Configuration;
        public BaseController(IConfiguration iconfiguration)
        {
            Configuration = iconfiguration;
        }
      }
}