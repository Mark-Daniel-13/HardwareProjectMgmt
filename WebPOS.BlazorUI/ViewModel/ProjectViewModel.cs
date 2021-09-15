using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPOS.BlazorUI.ViewModel
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int StatusId { get; set; }
        public static List<ProjectViewModel> ToModelList(List<Business.Project.Models.Projects> projects)
        {
            if (projects == null) return null;
            List<ProjectViewModel> VMList = new();
            return VMList = projects.Select(project => new ProjectViewModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Address = project.Address,
                StatusId = project.StatusId,
            }).ToList();
        }
        public static ProjectViewModel ToModel(Business.Project.Models.Projects project)
        {
            if (project == null) return null;
            return new ProjectViewModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Address = project.Address,
                StatusId = project.StatusId,
            };
        }
    }
    public class ProjectManageModel
    {
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(50, ErrorMessage = "Project name can only have maximum of 50 characters.")]
        public string Name { get; set; }
        [StringLength(66, ErrorMessage = "Project Address can only have maximum of 66 characters.")]
        public string Address { get; set; }
        public int StatusId { get; set; }
        public static ProjectManageModel ToModel(Business.Project.Models.Projects project)
        {
            if (project == null) return null;
            return new ProjectManageModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Address = project.Address,
                StatusId = project.StatusId,
            };
        }
        public static Business.Project.Models.Projects ToBusinessModel(ProjectManageModel project)
        {
            if (project == null) return null;
            return new Business.Project.Models.Projects
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Address = project.Address,
                StatusId = project.StatusId,
            };
        }
    }
    public class ProjectItemViewModel
    {
        public int ProjectItemId { get; set; }
        public int ProjectId { get; set; }
        public int ItemId { get; set; }
        public InventoryViewModel Item { get; set; }
        public string RequiredQty { get; set; }
        public string RemainingQty { get; set; }
        public static List<ProjectItemViewModel> ToModelList(List<Business.Project.Models.ProjectItems> projectItems)
        {
            if (projectItems == null) return null;
            List<ProjectItemViewModel> VMList = new();
            return VMList = projectItems.Select(item => new ProjectItemViewModel
            {
                ProjectItemId = item.ProjectItemId,
                ProjectId = item.ProjectId,
                ItemId = item.ItemId,
                Item = InventoryViewModel.ToModel(item.Item),
                RemainingQty = item.RemainingQty.ToString("N0"),
                RequiredQty = item.RequiredQty.ToString("N0"),
            }).ToList();
        }
        public static ProjectItemViewModel ToModel(Business.Project.Models.ProjectItems item)
        {
            if (item == null) return null;
            return new ProjectItemViewModel
            {
                ProjectItemId = item.ProjectItemId,
                ProjectId = item.ProjectId,
                ItemId = item.ItemId,
                Item = InventoryViewModel.ToModel(item.Item),
                RemainingQty = item.RemainingQty.ToString("N0"),
                RequiredQty = item.RequiredQty.ToString("N0"),
            };
        }
        public static Business.Project.Models.ProjectItems ToBusinessModel(ProjectItemViewModel item)
        {
            if (item == null) return null;
            return new Business.Project.Models.ProjectItems
            {
                ProjectItemId = item.ProjectItemId,
                ProjectId = item.ProjectId,
                ItemId = item.ItemId,
                RemainingQty = Convert.ToDouble(item.RemainingQty),
                RequiredQty = Convert.ToDouble(item.RequiredQty),
            };
        }
    }
    public class ProjectItemManageModel
    {
        public int ProjectItemId { get; set; }
        [Required(ErrorMessage = "Project Id is missing.")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Item Id is missing.")]
        public int ItemId { get; set; }
        [Required(ErrorMessage = "Required Quantity is missing.")]
        public string RequiredQty { get; set; }
        public string RemainingQty { get; set; }
        public static ProjectItemManageModel ToModel(Business.Project.Models.ProjectItems pi)
        {
            if (pi == null) return null;
            return new ProjectItemManageModel
            {
                ProjectItemId = pi.ProjectItemId,
                ProjectId = pi.ProjectId,
                ItemId = pi.ItemId,
                RequiredQty = pi.RequiredQty.ToString("N0"),
                RemainingQty = pi.RemainingQty.ToString("N0"),
            };
        }
        public static Business.Project.Models.ProjectItems ToBusinessModel(ProjectItemManageModel pi)
        {
            if (pi == null) return null;
            return new Business.Project.Models.ProjectItems
            {
                ProjectItemId = pi.ProjectItemId,
                ProjectId = pi.ProjectId,
                ItemId = pi.ItemId,
                RequiredQty = Convert.ToDouble(pi.RequiredQty),
                RemainingQty = Convert.ToDouble(pi.RemainingQty),
            };
        }
    }
}
