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

        public int AverageRating { get; set; }

        public int NumberOfRatings { get; set; }

        [ForeignKey("Albums")]
        public int AlbumID { get; set; }
        
    }
}
