using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenuBooking.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingID { get; set; }

        [Required]
        public int VenueID { get; set; }

        [Required]
        public int EventID { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        public Venue? Venue { get; set; }

        public Event? Event { get; set; }
    }
}