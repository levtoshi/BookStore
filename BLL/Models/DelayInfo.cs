using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL.Models;

namespace BLL.Models
{
    public class DelayInfo
    {
        public int Id { get; set; }
        public CustomerInfo Customer { get; set; }
        public int Amount { get; set; }
    }
}