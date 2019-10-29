using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poplawap.Backend.Model;
using Poplawap.Backend.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Poplawap.Backend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }

    }
}