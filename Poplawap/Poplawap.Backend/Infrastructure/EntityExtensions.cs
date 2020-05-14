using Microsoft.AspNetCore.Identity;
using Poplawap.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poplawap.Backend.Infrastructure
{
    public static class EntityExtensions
    {
        public static DTO.UserDTO MapUserResponse(this ApplicationUser user) =>
            new DTO.UserDTO 
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Verified = user.Verified,
                County = user.County                
            };

        public static DTO.CategoryDTO MapCategoryResponse(this Categories category) =>
            new DTO.CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategory = category.ParentCategory,
                Icon = category.Icon
            };

        public static DTO.SaleDTO MapSaleRespose(this Sales sale) =>
            new DTO.SaleDTO
            {
                ProductName = sale.Product.ProductName,
                Description = sale.Product.Description,
                PublishedDate = sale.Product.PublishedDate,
                EndDate = sale.Product.EndDate,
                Prize = sale.Product.Prize,
                Goal = sale.Goal,
                Status = sale.Status.Id,
                Images = sale.Product.ProductImages.Select(p => p.Base64).ToList()
            };
    }
}
