using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Project.Models
{
    public class ProjectItems
    {
        public int ProjectItemId { get; set; }
        public int ProjectId { get; set; }
        public int ItemId { get; set; }
        public Product.Models.Inventory Item { get; set; }
        public double RequiredQty { get; set; }
        public double RemainingQty { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public static List<ProjectItems> ToModelList(List<Data.Projects.Models.ProjectItems> models)
        {
            if (models == null) return null;
            List<ProjectItems> ModelList = new();
            return ModelList = models.Select(model => new ProjectItems
            {
                ProjectItemId = model.ProjectItemId,
                ProjectId = model.ProjectId,
                ItemId = model.ItemId,
                Item = Product.Models.Inventory.ToModel(model.Item),
                RequiredQty = model.RequiredQty,
                RemainingQty = model.RemainingQty,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                DeletedAt = model.DeletedAt,
            }).ToList();
        }
        public static ProjectItems ToModel(Data.Projects.Models.ProjectItems model)
        {
            if (model == null) return null;
            return new ProjectItems
            {
                ProjectItemId = model.ProjectItemId,
                ProjectId = model.ProjectId,
                ItemId = model.ItemId,
                Item = Product.Models.Inventory.ToModel(model.Item),
                RequiredQty = model.RequiredQty,
                RemainingQty = model.RemainingQty,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                DeletedAt = model.DeletedAt,
            };
        }
        public static Data.Projects.Models.ProjectItems ToDataModel(ProjectItems model)
        {
            if (model == null) return null;
            return new Data.Projects.Models.ProjectItems
            {
                ProjectItemId = model.ProjectItemId,
                ProjectId = model.ProjectId,
                ItemId = model.ItemId,
                RequiredQty = model.RequiredQty,
                RemainingQty = model.RemainingQty,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                DeletedAt = model.DeletedAt,
            };
        }
    }
}
