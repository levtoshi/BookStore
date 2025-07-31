using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SalesInfo
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime SoldTime { get; set; }
        public ProductInfo Product { get; set; }
    }
}