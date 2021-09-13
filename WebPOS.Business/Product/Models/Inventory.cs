using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Product.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public string UOM { get; set; }
        public double RetailPrice { get; set; }
        public double WholesalePrice { get; set; }
        public byte[] Image { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public static List<Inventory> ToModelList(List<Data.Product.Models.Inventory> invs)
        {
            if (invs == null) return null;
            List<Inventory> invModelList = new();
            return invModelList = invs.Select(inv => new Inventory
            {
                InventoryId = inv.InventoryId,
                Description = inv.Description,
                ProductId = inv.ProductId,
                Product = Product.ToModel(inv.Product),
                Quantity = inv.Quantity,
                UOM = inv.UOM,
                RetailPrice = inv.RetailPrice,
                WholesalePrice = inv.WholesalePrice,
                Image = inv.Image,
                ImageName = inv.ImageName,
                ImageType = inv.ImageType,
                CreatedAt = inv.CreatedAt,
                UpdatedAt = inv.UpdatedAt,
                DeletedAt = inv.DeletedAt,
            }).ToList();
        }
        public static Inventory ToModel(Data.Product.Models.Inventory inv)
        {
            if (inv == null) return null;
            return new Inventory
            {
                InventoryId = inv.InventoryId,
                Description = inv.Description,
                ProductId = inv.ProductId,
                Product = Product.ToModel(inv.Product),
                Quantity = inv.Quantity,
                UOM = inv.UOM,
                RetailPrice = inv.RetailPrice,
                WholesalePrice = inv.WholesalePrice,
                Image = inv.Image,
                ImageName = inv.ImageName,
                ImageType = inv.ImageType,
                CreatedAt = inv.CreatedAt,
                UpdatedAt = inv.UpdatedAt,
                DeletedAt = inv.DeletedAt,
            };
        }
        public static Data.Product.Models.Inventory ToDataModel(Inventory inv)
        {
            if (inv == null) return null;
            return new Data.Product.Models.Inventory
            {
                InventoryId = inv.InventoryId,
                Description = inv.Description,
                ProductId = inv.ProductId,
                Quantity = inv.Quantity,
                UOM = inv.UOM,
                RetailPrice = inv.RetailPrice,
                WholesalePrice = inv.WholesalePrice,
                Image = inv.Image,
                ImageName = inv.ImageName,
                ImageType = inv.ImageType,
                CreatedAt = inv.CreatedAt,
                UpdatedAt = inv.UpdatedAt,
                DeletedAt = inv.DeletedAt,
            };
        }
    }
}
