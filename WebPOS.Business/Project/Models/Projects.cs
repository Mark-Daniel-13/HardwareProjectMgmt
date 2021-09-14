using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Project.Models
{
    public class Projects
    {
        public int ProjectId { get; set; }
        public string Name { get; set;}
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public static List<Projects> ToModelList(List<Data.Projects.Models.Projects> models)
        {
            if (models == null) return null;
            List<Projects> ModelList = new();
            return ModelList = models.Select(model => new Projects
            {
                ProjectId = model.ProjectId,
                Name = model.Name,
                Address = model.Address,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                DeletedAt = model.DeletedAt,
            }).ToList();
        }
        public static Projects ToModel(Data.Projects.Models.Projects model)
        {
            if (model == null) return null;
            return new Projects
            {
                ProjectId = model.ProjectId,
                Name = model.Name,
                Address = model.Address,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                DeletedAt = model.DeletedAt,
            };
        }
        public static Data.Projects.Models.Projects ToDataModel(Projects model)
        {
            if (model == null) return null;
            return new Data.Projects.Models.Projects
            {
                ProjectId = model.ProjectId,
                Name = model.Name,
                Address = model.Address,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                DeletedAt = model.DeletedAt,
            };
        }
    }
}
