using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoThere.Models
{
    public class Event
    {

        [Required]
        public string Id { get; set; } = DateTime.UtcNow.Ticks.ToString();

        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(144, MinimumLength = 2)]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [StringLength(3000, MinimumLength = 2)]
        [Required]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(144, MinimumLength = 2)]
        [Required]
        public string Type { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDateTime{ get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDateTime { get; set; }

        [Required]
        public decimal Price { get; set; }

        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(144, MinimumLength = 2)]
        [Required]
        public string LocationName { get; set; }

        [Display(Name = "Street Address")]
        [StringLength(288, MinimumLength = 2)]
        [Required]
        public string StreetAddress { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(144, MinimumLength = 2)]
        [Required]
        public string City { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(144, MinimumLength = 2)]
        [Required]
        public string State { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(144, MinimumLength = 2)]
        [Required]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        [StringLength(144, MinimumLength = 2)]
        [Required]
        public string PostalCode { get; set; }

        //  TODO: how to get user input from text field and comma separated strings
        //  public List<string> Tags { get; set; }

    }
}