using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VenuBooking.Models
{
    public class EventType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventTypeID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}