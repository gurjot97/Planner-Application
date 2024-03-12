using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Planner_Application.Models
{
    public class EventViewModel
    {

        // Instance variables
        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        //[Required]
        //[StringLength(20)]
        //[Display(Name = "User Name")]
        //public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DefaultValue("None")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Weather")]
        public string? Weather { get; set; }

        [Display(Name = "Temperature")]
        public double? Temperature { get; set; }

        [Display(Name = "Location")]
        [DefaultValue("None")]
        public string Location { get; set; }

        // Instance variables
        [Required]
        [Display(Name = "Priority")]
        public string Priority { get; set; }

        [Display(Name = "Happy Quote")]
        public string? Quote { get; set; }
    }
}
