using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;   

namespace FitCore.Data.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime BookingDate { get; set; } //data rezervarii

        //relatie: legatura cu membrul (fk)
        public int MemberId { get; set; }
        public Member? Member { get; set; }

        //relatie: legatura cu clasa de fitness (fk)
        public int GymClassId { get; set; }
        public GymClass? GymClass { get; set; }
    }
}
