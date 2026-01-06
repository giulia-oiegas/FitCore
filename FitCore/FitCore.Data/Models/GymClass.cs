using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Trainer? Trainer { get; set; }

        //relatie: o clasa poate avea mai multe rezervari
        public ICollection<Booking>? Bookings { get; set; }

        [NotMapped]
        public string TimeInterval =>
            $"{Schedule:HH:mm} - {Schedule.AddMinutes(DurationMinutes):HH:mm}";

        [NotMapped]
        public int BookedSeats =>
            Bookings?.Count ?? 0;

        [NotMapped]
        public string CapacityText =>
            $"Locuri: {MaxCapacity - BookedSeats} / {MaxCapacity}";


        [NotMapped]
        public string Date =>
            Schedule.ToString("dddd, dd MMMM yyyy", CultureInfo.CurrentCulture);

    }
}
