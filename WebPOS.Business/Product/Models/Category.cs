using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Product.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public static List<Category> ToModelList(List<Data.Product.Models.Category> categories)
        {
            if (categories == null) return null;
            List<Category> categoryModelList = new();
            return categoryModelList = categories.Select(category => new Category
            {
                CategoryId = category.CategoryId,
                Name = category.CategoryName,
                Description = category.CategoryDescription,
                CreatedAt = category.CreatedAt,
                DeletedAt = category.DeletedAt,
                UpdatedAt = category.UpdatedAt,
            }).ToList();
        }
        public static Category ToModel(Data.Product.Models.Category category)
        {
            if (category == null) return null;
            return new Category
            {
                CategoryId = category.CategoryId,
                Name = category.CategoryName,
                Description = category.CategoryDescription,
                CreatedAt = category.CreatedAt,
                DeletedAt = category.DeletedAt,
                UpdatedAt = category.UpdatedAt,
            };
        }
        public static Data.Product.Models.Category ToDataModel(Category category)
        {
            if (category == null) return null;
            return new Data.Product.Models.Category
            {
                CategoryId = category.CategoryId,
                CategoryName = category.Name,
                CategoryDescription = category.Description,
                CreatedAt = category.CreatedAt,
                DeletedAt = category.DeletedAt,
                UpdatedAt = category.UpdatedAt,
            };
        }
    }
}
