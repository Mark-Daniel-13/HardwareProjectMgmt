using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPOS.BlazorUI.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public static List<CategoryViewModel> ToModelList(List<Business.Product.Models.Category> categories)
        {
            if (categories == null) return null;
            List<CategoryViewModel> categoryVMList = new();
            return categoryVMList = categories.Select(category => new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                CreatedAt = category.CreatedAt,
                DeletedAt = category.DeletedAt,
                UpdatedAt = category.UpdatedAt,
            }).ToList();
        }
        public static CategoryViewModel ToModel(Business.Product.Models.Category category)
        {
            if (category == null) return null;
            return new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                CreatedAt = category.CreatedAt,
                DeletedAt = category.DeletedAt,
                UpdatedAt = category.UpdatedAt,
            };
        }
    }
    public class CategoryManageModel {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category name is required.")]
        [StringLength(20,ErrorMessage = "Category name can only have maximum of 20 characters.")]
        public string Name { get; set; }
        [StringLength(60, ErrorMessage = "Category Description can only have maximum of 60 characters.")]
        public string Description { get; set; }
        public static CategoryManageModel ToModel(Business.Product.Models.Category category)
        {
            if (category == null) return null;
            return new CategoryManageModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
            };
        }
        public static Business.Product.Models.Category ToBusinessModel(CategoryManageModel category)
        {
            if (category == null) return null;
            return new Business.Product.Models.Category
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}
