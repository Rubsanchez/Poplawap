using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poplawap.Backend.Model;

namespace Poplawap.Backend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class SalesController : ControllerBase
    {
        ApplicationDbContext _db;

        public SalesController(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}