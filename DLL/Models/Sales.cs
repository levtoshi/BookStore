using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class Sales
    {
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime SoldTime { get; set; }
        [Required]
        public Product Product { get; set; }
    }
}