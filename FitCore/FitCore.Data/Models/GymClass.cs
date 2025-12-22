using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitCore.Data.Models
{
    public class GymClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } //ex: zumba de seara

        public DateTime Schedule { get; set; } //data si ora

        public int DurationMinutes { get; set; } //durata in minute

        public int MaxCapacity { get; set; } //capacitatea maxima a clasei

        //relatie: legatura cu antrenorul (fk)
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        //relatie: o clasa poate avea mai multe rezervari
        public ICollection<Booking> Bookings { get; set; }
    }
}
