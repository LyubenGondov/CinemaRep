using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaSite.Models;

namespace MovieApplication.Models
{
    public class Movie
    {
        private ICollection<Ticket> tickets;
        public Movie()
        {
            this.tickets = new HashSet<Ticket>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string MovieName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [MinLength(8, ErrorMessage = "Please add the start date of the movie in format - dd-MM-yyyy!")]
        public string StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [MinLength(8, ErrorMessage = "Please add the end date of the movie in format - dd-MM-yyyy!")]
        public string EndDate { get; set; }
        [Required]
        public string Ganre { get; set; }
        [Required]
        public bool IsMature { get; set; }
        [Required]
        [MaxLength(1, ErrorMessage = "The rating has to be between 1 and 5!")]
        public string Rating { get; set; }

        public virtual ICollection<Ticket> Tickets
        {
            get { return this.tickets; }
            set { this.tickets = value; }
        }
    }
}