using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_IEnumerable_IQueryable_Yield.Models
{
   public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Phone[] Phones { get; set; }
    }
}
