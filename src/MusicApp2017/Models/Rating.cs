using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApp2017.Models
{
    public class Rating
    {
        public int RatingID { get; set; }

        public decimal RatingValue { get; set; }

        [ForeignKey("Albums")]
        public int AlbumID { get; set; }

        [ForeignKey("AspNetUsers")]
        public string Id { get; set; }
    }
}
