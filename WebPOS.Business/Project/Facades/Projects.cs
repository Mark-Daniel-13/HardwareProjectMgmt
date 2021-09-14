using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Project.Facades
{
    public class Projects:IDisposable
    {
        private Data.Projects.Dataccess.Projects dataAccess = new();
        public async Task<List<Models.Projects>> GetAll(bool forceGet = false)
        {
            return Models.Projects.ToModelList(await dataAccess.GetAll(forceGet));
        }
        public async Task<Models.Projects> GetById(int projectId, bool forceGet = false)
        {
            return Models.Projects.ToModel(await dataAccess.GetById(projectId, forceGet));
        }
        public async Task<Helpers.ResultHandler> Update(Models.Projects project)
        {
            if (!isDataValid(project, true)) return new Helpers.ResultHandler().HandleResult(false, "There's an invalid input on your form.");

            project.UpdatedAt = DateTime.Now;
            try
            {
                var dataModel = Models.Projects.ToDataModel(project);
                var query = await dataAccess.Update(dataModel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Update, "project");
            }
            catch (Exception e)
            {
                return new Helpers.ResultHandler().HandleResult(false, e.Message);
            }
        }
        public async Task<Helpers.ResultHandler> Add(Models.Projects project)
        {
            if (!isDataValid(project)) return new Helpers.ResultHandler().HandleResult(false, "There's an invalid input on your form."); ;

            project.CreatedAt = DateTime.Now;
            project.UpdatedAt = DateTime.Now;
            try
            {
                var dataMOdel = Models.Projects.ToDataModel(project);
                var query = await dataAccess.Add(dataMOdel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Create, "project"); ;
            }
            catch (Exception e)
            {
                return new Helpers.ResultHandler().HandleResult(false, e.Message); ;
            }
        }
        public async Task<Helpers.ResultHandler> Remove(int projectId)
        {
            if (projectId == 0) return new Helpers.ResultHandler().HandleResult(false, "Please select a product to remove."); ;

            try
            {
                var dataMOdel = new Data.Projects.Models.Projects()
                {
                    ProjectId = projectId,
                    DeletedAt = DateTime.Now,
                };
                var query = await dataAccess.Remove(dataMOdel);
                return new Helpers.ResultHandler().HandleResult(query, Enums.ResultType.Delete, "project"); ;
            }
            catch (Exception err)
            {
                return new Helpers.ResultHandler().HandleResult(false, err.Message);
            }
        }

        private bool isDataValid(Models.Projects project, bool includeId = false)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(project.Name))
            {
                isValid = false;
            }
            if (includeId)
            {
                isValid = (project.ProjectId > 0) ? true : false;
            }
            return isValid;
        }
        public void Dispose()
        {
            dataAccess = null;
        }
    }
}
