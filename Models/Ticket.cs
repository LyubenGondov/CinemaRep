using System.ComponentModel.DataAnnotations;
using MovieApplication.Models;

namespace CinemaSite.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        
        public string MovieName { get; set; }
        [Required]
        public bool IsPromotional { get; set; }
        [Required]
        [MaxLength(2,ErrorMessage = "The places are only 20!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Place in the hall must be numeric")]
        public string PlaceInTheHall { get; set; }
        [Required]
        [MaxLength(2,ErrorMessage = "The rows are only 10!")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Rows must be numeric")]
        public string Row { get; set; }
        public int MovieId { get; set; }
        public Movie Movies { get; set; }
        public Hall Halls { get; set; }
    }
}