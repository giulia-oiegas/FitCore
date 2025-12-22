using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FitCore.Data.Models
{
    public class Trainer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
      
        public string Specialization { get; set; } //ex: yoga, bodybuilding, cardio etc.
      
        //relatie: un antrenor poate tine mai multe clase
        public ICollection<GymClass> GymClasses { get; set; }
    }
}
