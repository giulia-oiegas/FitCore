using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FitCore.Data.Models
{
    public class MembershipType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } //ex: Standard, Student, VIP

        public int DurationMonths { get; set; } //durata in luni

        [DataType(DataType.Currency)]
        public decimal Price { get; set; } //pretul abonamentului

        public string? Description { get; set; } //descriere optionala
    }
}
