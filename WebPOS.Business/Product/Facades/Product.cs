using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Product.Facades
{
    public class Product:IDisposable
    {
        private Data.Product.DataAccess.Product dataAccess = new();
        public async Task<List<Models.Product>> GetAll(bool withCategory = false,bool forceGet = false)
        {
            //force get is to either retrieve soft deleted data or not
            return withCategory ? Models.Product.ToModelList(await dataAccess.GetAllWithCategory())
                                : Models.Product.ToModelList(await dataAccess.GetAll());
        }
        public async Task<Models.Product> GetById(int productId, bool withCategory = false)
        {
            return withCategory ? Models.Product.ToModel(await dataAccess.GetByIdWithCategory(productId))
                                : Models.Product.ToModel(await dataAccess.GetById(productId));
        }
        public async Task<Helpers.ResultHandler> UpdateProduct(Models.Product product)
        {
            if (!isDataValid(product,true)) return new Helpers.ResultHandler().HandleResult(false, "There's an invalid input on your form.");

            product.UpdatedAt = DateTime.Now;
            try
            {
                var dataModel = Models.Product.ToDataModel(product);
                var query = await dataAccess.UpdateProduct(dataModel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Update, "product");
            }
            catch(Exception e)
            {
                return new Helpers.ResultHandler().HandleResult(false, e.Message);
            }
        }
        public async Task<Helpers.ResultHandler> AddProduct(Models.Product product)
        {
            if (!isDataValid(product)) return new Helpers.ResultHandler().HandleResult(false, "There's an invalid input on your form."); ;

            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            try
            {
                var dataMOdel = Models.Product.ToDataModel(product);
                var query = await dataAccess.AddProduct(dataMOdel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Create, "product"); ;
            }
            catch(Exception e)
            {
                return new Helpers.ResultHandler().HandleResult(false,e.Message); ;
            }
        }
        public async Task<Helpers.ResultHandler> RemoveProduct(int productId)
        {
            if (productId == 0) return new Helpers.ResultHandler().HandleResult(false, "Please select a product to remove."); ;

            try
            {
                var dataMOdel = new Data.Product.Models.Product()
                {
                    ProductId = productId,
                    DeletedAt = DateTime.Now,
                };
                var query = await dataAccess.RemoveProduct(dataMOdel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Delete, "product"); ;
            }
            catch (Exception err)
            {
                return new Helpers.ResultHandler().HandleResult(false,err.Message);
            }
        }

        private bool isDataValid(Models.Product product,bool includeId = false) {
            bool isValid = true;
            if (string.IsNullOrEmpty(product.ProductName) || product.CategoryId == 0) {
                isValid = false;
            }
            if (includeId) {
                isValid = (product.ProductId > 0) ? true : false;
            }
            return isValid;
        }
        public void Dispose()
        {
            dataAccess = null;
        }
    }
}
