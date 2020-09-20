using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.DAL.Entities
{
    public class Product
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
