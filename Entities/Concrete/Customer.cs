using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class Customer : IEntity
    {
        [Key]
        public long TC { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        
    }
}
