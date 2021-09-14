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
        public static List<ProjectViewModel> ToModelList(List<Business.Project.Models.Projects> projects)
        {
            if (projects == null) return null;
            List<ProjectViewModel> VMList = new();
            return VMList = projects.Select(project => new ProjectViewModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Address = project.Address,
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
        public static ProjectManageModel ToModel(Business.Project.Models.Projects project)
        {
            if (project == null) return null;
            return new ProjectManageModel
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                Address = project.Address,
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
            };
        }
    }
}
