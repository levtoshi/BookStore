using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class BookInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FullNameInfo Author { get; set; }
        public ProducerInfo Producer { get; set; }
        public short PageAmount { get; set; }
        public GenreInfo Genre { get; set; }
        public short Year { get; set; }
        public string? IsContinuation { get; set; }
    }
}