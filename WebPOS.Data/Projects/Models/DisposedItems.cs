using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Data.Projects.Models
{
    public class DisposedItems
    {
        public int DisposedItemsId { get; set; }
        public int ItemId { get; set; }
        public int ProjectId { get; set;}
        public double Quantity { get; set; }
        public DateTime CreatedAt { get; set;}
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
