using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Data.Projects.Models
{
    public class ProjectItems
    {
        public int ProjectItemId { get; set;}
        public int ProjectId { get; set;}
        public int ItemId { get; set; }
        public Product.Models.Inventory Item { get; set; }
        public double RequiredQty { get; set; }
        public double RemainingQty { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
