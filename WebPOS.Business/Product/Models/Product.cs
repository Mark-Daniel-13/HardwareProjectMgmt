using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Product.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
        public bool isAvailable { get; set; }
        public double Quantity { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Category Category { get; set; }
        public static List<Product> ToModelList(List<Data.Product.Models.Product> products)
        {
            if (products == null) return null;
            List<Product> productModelList = new();
            return productModelList = products.Select(product => new Product
            {
               ProductId = product.ProductId,
               ProductName = product.ProductName,
               ProductDescription = product.ProductDescription,
               CategoryId = product.CategoryId,
               Category = Category.ToModel(product.Category),
               isAvailable = product.isAvailable,
               Quantity = product.Quantity,
               Image = product.Image,
               ImageType = product.ImageType,
               ImageName = product.ImageName,
               CreatedAt = product.CreatedAt,
               UpdatedAt = product.UpdatedAt,
               DeletedAt = product.DeletedAt,
            }).ToList();
        }
        public static Product ToModel(Data.Product.Models.Product product)
        {
            if (product == null) return null;
            return new Product
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                CategoryId = product.CategoryId,
                Category = Category.ToModel(product.Category),
                isAvailable = product.isAvailable,
                Quantity = product.Quantity,
                Image = product.Image,
                ImageType = product.ImageType,
                ImageName = product.ImageName,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                DeletedAt = product.DeletedAt,
            };
        }
        public static Data.Product.Models.Product ToDataModel(Product product)
        {
            if (product == null) return null;
            return new Data.Product.Models.Product
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                CategoryId = product.CategoryId,
                isAvailable = product.isAvailable,
                Quantity = product.Quantity,
                Image = product.Image,
                ImageType = product.ImageType,
                ImageName = product.ImageName,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                DeletedAt = product.DeletedAt,
            };
        }
    }
}
