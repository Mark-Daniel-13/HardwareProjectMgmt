using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Product.Facades
{
    public class Inventory:IDisposable
    {
        private Data.Product.DataAccess.Inventory dataAccess = new();
        public async Task<List<Models.Inventory>> GetAll(bool withCategory = false, bool forceGet = false)
        {
            return Models.Inventory.ToModelList(await dataAccess.GetAllWithProduct(withCategory));
        }
        public async Task<Models.Inventory> GetById(int inventoryId, bool withCategory = false)
        {
            return Models.Inventory.ToModel(await dataAccess.GetByIdWithProduct(inventoryId, withCategory));
        }
        public async Task<Helpers.ResultHandler> UpdateInventory(Models.Inventory inventory)
        {
            if (!isDataValid(inventory, true)) return new Helpers.ResultHandler().HandleResult(false, "There's an invalid input on your form.");

            inventory.UpdatedAt = DateTime.Now;
            try
            {
                var dataModel = Models.Inventory.ToDataModel(inventory);
                var query = await dataAccess.UpdateInventory(dataModel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Update, "inventory");
            }
            catch (Exception e)
            {
                return new Helpers.ResultHandler().HandleResult(false, e.Message);
            }
        }
        public async Task<Helpers.ResultHandler> AddInventory(Models.Inventory inventory)
        {
            if (!isDataValid(inventory)) return new Helpers.ResultHandler().HandleResult(false, "There's an invalid input on your form."); ;

            inventory.CreatedAt = DateTime.Now;
            inventory.UpdatedAt = DateTime.Now;
            try
            {
                var dataMOdel = Models.Inventory.ToDataModel(inventory);
                var query = await dataAccess.AddInventory(dataMOdel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Create, "inventory"); ;
            }
            catch (Exception e)
            {
                return new Helpers.ResultHandler().HandleResult(false, e.Message); ;
            }
        }
        public async Task<Helpers.ResultHandler> RemoveInventory(int inventoryId)
        {
            if (inventoryId == 0) return new Helpers.ResultHandler().HandleResult(false, "Please select a inventory to remove."); ;

            try
            {
                var dataMOdel = new Data.Product.Models.Inventory()
                {
                    InventoryId = inventoryId,
                    DeletedAt = DateTime.Now,
                };
                var query = await dataAccess.RemoveInventory(dataMOdel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Delete, "inventory"); ;
            }
            catch (Exception err)
            {
                return new Helpers.ResultHandler().HandleResult(false, err.Message);
            }
        }

        private bool isDataValid(Models.Inventory inventory, bool includeId = false)
        {
            bool isValid = true;
            if (inventory.ProductId == 0)
            {
                isValid = false;
            }
            if (includeId)
            {
                isValid = (inventory.InventoryId > 0) ? true : false;
            }
            return isValid;
        }
        public void Dispose()
        {
            dataAccess = null;
        }
    }
}
