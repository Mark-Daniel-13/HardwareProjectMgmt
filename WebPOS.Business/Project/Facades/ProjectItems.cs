using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Project.Facades
{
    public class ProjectItems:IDisposable
    {
        private Data.Projects.Dataccess.ProjectItems dataAccess = new();
        public async Task<List<Models.ProjectItems>> GetAll(bool forceGet = false)
        {
            return Models.ProjectItems.ToModelList(await dataAccess.GetAll(forceGet));
        }
        public async Task<Models.ProjectItems> GetById(int projectItemId,bool withInventory = false, bool forceGet = false)
        {
            return (!withInventory) ? Models.ProjectItems.ToModel(await dataAccess.GetById(projectItemId, forceGet)) : Models.ProjectItems.ToModel(await dataAccess.GetByIdWithInventory(projectItemId));
        }
        public async Task<List<Models.ProjectItems>> GetByProjectIdWithInventory(int projectId)
        {
            return Models.ProjectItems.ToModelList(await dataAccess.GetByProjectIdWithInventory(projectId));
        }
        public async Task<Helpers.ResultHandler> Update(Models.ProjectItems projectItems)
        {
            if (!isDataValid(projectItems, true)) return new Helpers.ResultHandler().HandleResult(false, "There's an invalid input on your form.");

            projectItems.UpdatedAt = DateTime.Now;
            try
            {
                var dataModel = Models.ProjectItems.ToDataModel(projectItems);
                var query = await dataAccess.Update(dataModel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Update, "project item");
            }
            catch (Exception e)
            {
                return new Helpers.ResultHandler().HandleResult(false, e.Message);
            }
        }
        public async Task<Helpers.ResultHandler> Add(Models.ProjectItems projectItems)
        {
            if (!isDataValid(projectItems)) return new Helpers.ResultHandler().HandleResult(false, "There's an invalid input on your form."); ;

            projectItems.CreatedAt = DateTime.Now;
            projectItems.UpdatedAt = DateTime.Now;
            projectItems.RemainingQty = projectItems.RequiredQty;
            try
            {
                var dataMOdel = Models.ProjectItems.ToDataModel(projectItems);
                var query = await dataAccess.Add(dataMOdel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Create, "project item"); ;
            }
            catch (Exception e)
            {
                return new Helpers.ResultHandler().HandleResult(false, e.Message); ;
            }
        }
        public async Task<Helpers.ResultHandler> Remove(int projectItemId)
        {
            if (projectItemId == 0) return new Helpers.ResultHandler().HandleResult(false, "Please select a product to remove."); ;

            try
            {
                var dataMOdel = new Data.Projects.Models.ProjectItems()
                {
                    ProjectItemId = projectItemId,
                    DeletedAt = DateTime.Now,
                };
                var query = await dataAccess.Remove(dataMOdel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Delete, "project item"); ;
            }
            catch (Exception err)
            {
                return new Helpers.ResultHandler().HandleResult(false, err.Message);
            }
        }

        private bool isDataValid(Models.ProjectItems pi, bool includeId = false)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(pi.ProjectId.ToString()) || string.IsNullOrEmpty(pi.ItemId.ToString()) || string.IsNullOrEmpty(pi.RequiredQty.ToString()))
            {
                isValid = false;
            }
            if (includeId)
            {
                isValid = (pi.ProjectItemId > 0) ? true : false;
            }
            return isValid;
        }
        public void Dispose()
        {
            dataAccess = null;
        }
    }
}
