using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_IEnumerable_IQueryable_Yield.Models
{
   public class Address
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}
