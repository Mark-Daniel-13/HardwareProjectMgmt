using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Product.Facades
{
    public class Category:IDisposable
    {
        private Data.Product.DataAccess.Category dataAccess = new();
        public async Task<List<Models.Category>> GetAll(bool forceGet = false)
        {
            //force get is to either retrieve soft deleted data or not
            return Models.Category.ToModelList(await dataAccess.GetAll());
        }
        public async Task<Models.Category> GetById(int userId)
        {
            return Models.Category.ToModel(await dataAccess.GetById(userId));
        }
        public async Task<Helpers.ResultHandler> UpdateCategory(Models.Category category) {
            if (string.IsNullOrEmpty(category.Name)) return new Helpers.ResultHandler().HandleResult(false, "Please enter category name.");

            category.UpdatedAt = DateTime.Now;
            try
            {
                var dataModel = Models.Category.ToDataModel(category);
                var query = await dataAccess.UpdateCategory(dataModel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Update, "category");
            }
            catch(Exception e){
                return new Helpers.ResultHandler().HandleResult(false, e.Message); ;
            }
        }
        public async Task<Helpers.ResultHandler> AddCategory(Models.Category category) {
            if (string.IsNullOrEmpty(category.Name)) return new Helpers.ResultHandler().HandleResult(false, "Please enter category name."); ;

            category.CreatedAt = DateTime.Now;
            category.UpdatedAt = DateTime.Now;
            try {
                var dataMOdel = Models.Category.ToDataModel(category);
                var query = await dataAccess.AddCategory(dataMOdel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Create, "category");
            } catch(Exception e) {
                return new Helpers.ResultHandler().HandleResult(false, e.Message);
            }
        }
        public async Task<Helpers.ResultHandler> RemoveCategory(int categoryId)
        {
            if (categoryId == 0) return new Helpers.ResultHandler().HandleResult(false, "Please select a category to remove.");
            try
            {
                var dataMOdel = new Data.Product.Models.Category() { 
                    CategoryId = categoryId,
                    DeletedAt = DateTime.Now,
                };
                var query = await dataAccess.RemoveCategory(dataMOdel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Delete, "category"); ;
            }
            catch(Exception err)
            {
                return new Helpers.ResultHandler().HandleResult(false, err.Message);
            }
        }

        public void Dispose()
        {
            dataAccess = null;
        }
    }
}
