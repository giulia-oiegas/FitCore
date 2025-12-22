using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FitCore.Data.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Prenumele este obligatoriu.")]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; } // "?" inseamna ca poate fi null
        
        public DateTime Birthday { get; set; }

        //relatie: un membru poate avea mai multe rezervari
        public ICollection<Booking>? Bookings { get; set; }
    }
}
