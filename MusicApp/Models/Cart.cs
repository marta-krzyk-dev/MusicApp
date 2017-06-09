using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MusicApp.Models
{
    public class Cart
    {
        [Key] //by default CartId or ID field is treated as the key
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int AlbumId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual Album Album { get; set; } //virtual enables Entity Framework to lazy load
    }
}