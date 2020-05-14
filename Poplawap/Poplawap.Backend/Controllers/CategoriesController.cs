using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poplawap.Backend.Model;
using Poplawap.Backend.Infrastructure;
using Poplawap.DTO;

namespace Poplawap.Backend.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        ApplicationDbContext _db;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }


        /// <summary>
        /// Get the product categories
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetMainCategories()
        {
            return await _db.Categories.AsNoTracking()
                                     .Where(c => c.ParentCategory == null)
                                     .Select(c => c.MapCategoryResponse())
                                     .ToListAsync();            
        }

    }
}