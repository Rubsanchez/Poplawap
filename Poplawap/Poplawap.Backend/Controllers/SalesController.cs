using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Poplawap.Backend.Helpers;
using Poplawap.Backend.Infrastructure;
using Poplawap.Backend.Model;
using Poplawap.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Poplawap.Backend.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    public class SalesController : ControllerBase
    {
        ApplicationDbContext _db;

        public SalesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale(CreateSaleDTO saleDTO)
        {

            if (!ModelState.IsValid)
                return BadRequest(Utils.GetResponse("EmptySale", "The sale is null"));

            ApplicationUser user = await _db.Users.AsNoTracking()
                                            .SingleOrDefaultAsync(u => u.Email == saleDTO.UserEmail.ToUpper());

            if (user == null)
                return BadRequest(Utils.GetResponse("UserNotFound", "The user not exists"));

            Products product = new Products
            {
                ProductName = saleDTO.ProductName,
                Description = saleDTO.Description,
                Prize = saleDTO.Prize,
                PublishedDate = DateTime.Now,
                EndDate = saleDTO.EndDate
            };


            if (saleDTO.Images != null && saleDTO.Images.Count > 0)
            {
                List<ProductImages> images = new List<ProductImages>();

                for (int i = 0; i < saleDTO.Images.Count; i++)
                {
                    images.Add(new ProductImages
                    {
                        Base64 = saleDTO.Images[i],
                        ImageAlt = saleDTO.ProductName,
                    });
                }
                product.ProductImages = images;
            }

            product = _db.Products.Add(product).Entity;

            Sales sale = new Sales
            {
                Goal = saleDTO.Goal,
                Product = product,
                UserId = user.Id,
                StatusId = 1 //Opened
            };            

            sale = _db.Sales.Add(sale).Entity;

            List<SalesCategories> categories = new List<SalesCategories>();
            for (int i = 0; i < saleDTO.Categories.Count; i++)
            {
                categories.Add(new SalesCategories
                {
                    CategoryId = saleDTO.Categories[i],
                    SaleId = sale.Id
                });
            }

            sale.Categories = categories;

            _db.SaveChanges();

            return Ok();

        }

        [HttpGet("{id}")]
        public async Task<SaleDTO> GetSale(int id)
        {
            Sales sale = await _db.Sales.AsNoTracking()
                                    .Include(s=> s.Status)
                                    .Include(s => s.Product)
                                    .ThenInclude(p => p.ProductImages)
                                    .SingleOrDefaultAsync(s => s.Id == id);

            if (sale == null)
                return null;            

            return sale.MapSaleRespose();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<SaleDTO>> GetSales()
        {
            return await _db.Sales.AsNoTracking()
                            .Include(s => s.Status)
                            .Include(s => s.Product)
                            .ThenInclude(p => p.ProductImages)
                            .Select(s => s.MapSaleRespose())
                            .ToListAsync();            
        }

        [HttpGet("{category}")]
        public async Task<List<SaleDTO>> GetSalesByCategory(int category)
        {
            return await _db.Sales.AsNoTracking()
                            .Include(s => s.Status)
                            .Include(s => s.Product)
                            .ThenInclude(p => p.ProductImages)
                            .Where(s=> s.Categories.Any(c=> c.CategoryId == category))
                            .Select(s => s.MapSaleRespose())
                            .ToListAsync();
        }

    }
}