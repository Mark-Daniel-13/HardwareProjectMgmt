using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPOS.BlazorUI.ViewModel
{
    public class InventoryViewModel
    {
        public int InventoryId { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public ProductVIewModel Product { get; set; }
        public double Quantity { get; set; }
        public string UOM { get; set; }
        public double RetailPrice { get; set; }
        public double WholesalePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public static List<InventoryViewModel> ToModelList(List<Business.Product.Models.Inventory> inventories)
        {
            if (inventories == null) return null;
            List<InventoryViewModel> inventoryVMList = new();
            return inventoryVMList = inventories.Select(inv => new InventoryViewModel
            {
                InventoryId = inv.InventoryId,
                Description = inv.Description,
                ProductId = inv.ProductId,
                Product = ProductVIewModel.ToModel(inv.Product),
                Quantity = inv.Quantity,
                UOM = inv.UOM,
                RetailPrice = inv.RetailPrice,
                WholesalePrice = inv.WholesalePrice,
                CreatedAt = inv.CreatedAt,
                UpdatedAt = inv.UpdatedAt,
                DeletedAt = inv.DeletedAt
            }).ToList();
        }
        public static InventoryViewModel ToModel(Business.Product.Models.Inventory inventory)
        {
            if (inventory == null) return null;
            return new InventoryViewModel
            {
                InventoryId = inventory.InventoryId,
                Description = inventory.Description,
                ProductId = inventory.ProductId,
                Product = ProductVIewModel.ToModel(inventory.Product),
                Quantity = inventory.Quantity,
                UOM = inventory.UOM,
                RetailPrice = inventory.RetailPrice,
                WholesalePrice = inventory.WholesalePrice,
                CreatedAt = inventory.CreatedAt,
                UpdatedAt = inventory.UpdatedAt,
                DeletedAt = inventory.DeletedAt
            };
        }
    }
    public class InventoryManageModel
    {
        public int InventoryId { get; set; }
        [Required(ErrorMessage = "Inventory description is required.")]
        [StringLength(150, ErrorMessage = "Inventory description can only have maximum of 150 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Product is required.")]
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public string UOM { get; set; }
        public double RetailPrice { get; set; }
        public double WholesalePrice { get; set; }
        public static InventoryManageModel ToModel(Business.Product.Models.Inventory inventory)
        {
            if (inventory == null) return null;
            return new InventoryManageModel
            {
                InventoryId = inventory.InventoryId,
                Description = inventory.Description,
                Quantity = inventory.Quantity,
                UOM = inventory.UOM,
                RetailPrice = inventory.RetailPrice,
                WholesalePrice = inventory.WholesalePrice,
                ProductId = inventory.ProductId,
            };
        }
        public static Business.Product.Models.Inventory ToBusinessModel(InventoryManageModel inventory)
        {
            if (inventory == null) return null;
            return new Business.Product.Models.Inventory
            {
                InventoryId = inventory.InventoryId,
                Description = inventory.Description,
                Quantity = Convert.ToDouble(inventory.Quantity),
                UOM = inventory.UOM,
                RetailPrice = inventory.RetailPrice,
                WholesalePrice = inventory.WholesalePrice,
                ProductId = inventory.ProductId,
            };
        }
    }
}
