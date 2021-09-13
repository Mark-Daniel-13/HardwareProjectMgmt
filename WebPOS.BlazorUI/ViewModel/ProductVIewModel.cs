using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPOS.BlazorUI.ViewModel
{
    public class ProductVIewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public CategoryViewModel Category{get;set;}
        public static List<ProductVIewModel> ToModelList(List<Business.Product.Models.Product> products)
        {
            if (products == null) return null;
            List<ProductVIewModel> productVMList = new();
            return productVMList = products.Select(product => new ProductVIewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Category = CategoryViewModel.ToModel(product.Category),
            }).ToList();
        }
        public static ProductVIewModel ToModel(Business.Product.Models.Product product)
        {
            if (product == null) return null;
            return new ProductVIewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Category = CategoryViewModel.ToModel(product.Category),
            };
        }
    }
    public class ProductManageModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(20, ErrorMessage = "Product name can only have maximum of 20 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
        public static ProductManageModel ToModel(Business.Product.Models.Product product)
        {
            if (product == null) return null;
            return new ProductManageModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                CategoryId = product.CategoryId,
            };
        }
        public static Business.Product.Models.Product ToBusinessModel(ProductManageModel product)
        {
            if (product == null) return null;
            return new Business.Product.Models.Product
            {
                ProductId = product.ProductId,
                Name = product.Name,
                CategoryId = product.CategoryId,
            };
        }
    }
}
