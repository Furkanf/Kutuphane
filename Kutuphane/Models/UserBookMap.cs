using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kutuphane.Models
{
    public class UserBookMap
    {

        [Column(Order = 0)]
        [Key, ForeignKey("book")]
        public string Isbn { get; set; }
        
        public virtual Book book { get; set; }

        [Column(Order = 1)]
        [ForeignKey("user")]
        public string userId { get; set; }

        
        public virtual StandardUser user { get; set; }
        public DateTime deliveryDate { get; set; }
    }
}