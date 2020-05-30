using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace PortalRandkowy.API.Controllers
{

    [ApiController]
    //http://localhost:5000/api
    [Route("api/[controller]")]
    public class ValuesController: ControllerBase
    {
        public ActionResult<string[]> Get()
        {
            return new string[] {"value1","value2"};
        }
    }
}