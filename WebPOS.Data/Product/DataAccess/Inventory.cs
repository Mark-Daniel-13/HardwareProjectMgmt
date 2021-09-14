using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Data.Product.DataAccess
{
    public class Inventory:GeneralDataAccess
    {
        string connString = ConfigurationManager.ConnectionStrings["App_DB"]?.ConnectionString;
        public async Task<List<Models.Inventory>> GetAll(bool forceGet = false)
        {
            string sqlSyntax = (forceGet) ? "Select * from inventories" : "Select * from inventories Where DeletedAt IS NULL Order By ProductId desc";
            return await LoadData<Models.Inventory, dynamic>(sqlSyntax, new { }, connString);
        }
        public async Task<List<Models.Inventory>> GetAllWithProduct(bool category=false)
        {
            string sqlSyntaxFull = @"Select i.*,p.*,c.* from inventories i 
                                        inner join products p on i.ProductId = p.ProductId
                                        inner join categories c on p.CategoryId = c.CategoryId
                                        Where p.deletedAt IS NULL AND c.deletedAt IS NULL AND i.deletedAt IS NULL Order By i.InventoryId desc";
            string sqlSyntax = @"Select i.*,p.* from inventories i 
                                        inner join products p on i.ProductId = p.ProductId
                                        Where p.deletedAt IS NULL AND i.deletedAt IS NULL Order By i.InventoryId desc";
            string _sqlSyntax = category ? sqlSyntaxFull : sqlSyntax;
            using (IDbConnection conn = new MySqlConnection(connString))
            {
                var datas = await conn.QueryAsync<Models.Inventory, Models.Product,Models.Category, Models.Inventory>
                            (_sqlSyntax, (inventory, product, category) => { product.Category = category; inventory.Product = product; return inventory; }, splitOn: "ProductId,CategoryId");
                return datas.ToList();
            }
        }
        public async Task<Models.Inventory> GetByIdWithProduct(int inventoryId,bool category = false)
        {
            var p = new { InventoryId = inventoryId };
            string sqlSyntaxFull = @"Select i.*,p.*,c.* from inventories i 
                                        inner join products p on i.ProductId = p.ProductId
                                        inner join categories c on p.CategoryId = c.CategoryId
                                        Where p.deletedAt IS NULL AND c.deletedAt IS NULL AND i.deletedAt IS NULL AND i.InventoryId = @InventoryId Order By i.InventoryId desc";
            string sqlSyntax = @"Select i.*,p.* from inventories i 
                                        inner join products p on i.ProductId = p.ProductId
                                        Where p.deletedAt IS NULL AND i.deletedAt IS NULL AND i.InventoryId = @InventoryId Order By i.InventoryId desc";
            string _sqlSyntax = category ? sqlSyntaxFull : sqlSyntax;
            using (IDbConnection conn = new MySqlConnection(connString))
            {
                var datas = await conn.QueryAsync<Models.Inventory, Models.Product, Models.Category, Models.Inventory>
                            (_sqlSyntax, (inventory, product, category) => { product.Category = category; inventory.Product = product; return inventory; }, p, splitOn: "ProductId,CategoryId");
                return datas?.FirstOrDefault();
            }
        }
        public async Task<Models.Inventory> GetById(int inventoryId, bool forceGet = false)
        {
            var p = new { InventoryId = inventoryId };
            string sqlSyntax = (forceGet) ? @"Select * from inventories Where InventoryId=@InventoryId" : @"Select * from inventories Where InventoryId=@InventoryId And DeletedAt IS NULL";
            var result = await LoadData<Models.Inventory, dynamic>(sqlSyntax, p, connString);
            return result?.FirstOrDefault();
        }
        public async Task<bool> UpdateInventory(Models.Inventory inventory)
        {
            var p = inventory;
            string sqlSyntax = @"Update inventories Set ProductId=@ProductId,Description=@Description,Quantity=@Quantity,UOM=@UOM,RetailPrice=@RetailPrice,WholesalePrice=@WholesalePrice,
                                updatedAt=@UpdatedAt Where InventoryId=@InventoryId";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
        public async Task<bool> AddInventory(Models.Inventory inventory)
        {
            var p = inventory;
            string sqlSyntax = @"Insert Into inventories(ProductId,Description,Quantity,UOM,RetailPrice,WholesalePrice,createdAt,updatedAt)
                                    VALUES(@ProductId,@Description,@Quantity,@UOM,@RetailPrice,@WholesalePrice,@CreatedAt,@UpdatedAt)";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
        public async Task<bool> RemoveInventory(Models.Inventory inventory)
        {
            var p = inventory;
            string sqlSyntax = @"Update inventories Set deletedAt=@DeletedAt Where InventoryId=@InventoryId";
            var result = await ModifyData(sqlSyntax, p, connString);
            return result == 0 ? false : true;
        }
    }
}
