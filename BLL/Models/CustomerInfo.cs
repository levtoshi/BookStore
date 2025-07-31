using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CustomerInfo
    {
        public int Id { get; set; }
        public FullNameInfo FullName { get; set; }
        public string Email { get; set; }
    }
}