using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kutuphane.Models
{
    public class Book
    {
        [Key]
        public string Isbn { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public byte[] Image { get; set; }

        public virtual UserBookMap userMap { get; set; }
    }
}