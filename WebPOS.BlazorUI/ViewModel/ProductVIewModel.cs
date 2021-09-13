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
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public CategoryViewModel Category{get;set;}
        public bool isAvailable { get; set; }
        public double Quantity { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        public static List<ProductVIewModel> ToModelList(List<Business.Product.Models.Product> products)
        {
            if (products == null) return null;
            List<ProductVIewModel> productVMList = new();
            return productVMList = products.Select(product => new ProductVIewModel
            {
                ProductId = product.ProductId,
                Name = product.ProductName,
                Description = product.ProductDescription,
                CategoryId = product.CategoryId,
                Category = CategoryViewModel.ToModel(product.Category),
                isAvailable = product.isAvailable,
                Quantity=product.Quantity,
                Image = product.Image,
                ImageName = product.ImageName,
            }).ToList();
        }
        public static ProductVIewModel ToModel(Business.Product.Models.Product product)
        {
            if (product == null) return null;
            return new ProductVIewModel
            {
                ProductId = product.ProductId,
                Name = product.ProductName,
                Description = product.ProductDescription,
                CategoryId = product.CategoryId,
                Category = CategoryViewModel.ToModel(product.Category),
                isAvailable = product.isAvailable,
                Quantity = product.Quantity,
                Image = product.Image,
                ImageName = product.ImageName,
            };
        }
    }
    public class ProductManageModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(20, ErrorMessage = "Product name can only have maximum of 20 characters.")]
        public string Name { get; set; }
        [StringLength(60, ErrorMessage = "Product Description can only have maximum of 60 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Product quantity is required.")]
        public string Quantity { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
        public bool isAvailable { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        //public HttpPostedFileBase
        public static ProductManageModel ToModel(Business.Product.Models.Product product)
        {
            if (product == null) return null;
            return new ProductManageModel
            {
                ProductId = product.ProductId,
                Name = product.ProductName,
                Description = product.ProductDescription,
                CategoryId = product.CategoryId,
                isAvailable = product.isAvailable,
                Quantity = product.Quantity.ToString("N0"),
                Image = product.Image,
                ImageName = product.ImageName,
                ImageType = product.ImageType,
            };
        }
        public static Business.Product.Models.Product ToBusinessModel(ProductManageModel product)
        {
            if (product == null) return null;
            return new Business.Product.Models.Product
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                CategoryId = product.CategoryId,
                isAvailable = product.isAvailable,
                Quantity = Convert.ToDouble(product.Quantity),
                Image = product.Image,
                ImageName = product.ImageName,
                ImageType = product.ImageType,
            };
        }
    }
}
