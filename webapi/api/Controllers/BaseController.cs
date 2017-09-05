using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using webapi.business.BAL;
using webapi.common;

namespace webapi.api.Controllers
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