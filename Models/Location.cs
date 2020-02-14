using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoThere.Models
{
    public class Location
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Street Address")]
        [StringLength(200, MinimumLength = 3)]
        [Required]
        public string StreetAddress { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(200, MinimumLength = 2)]
        [Required]
        public string City { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(200, MinimumLength = 3)]
        [Required]
        public string State { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(200, MinimumLength = 3)]
        [Required]
        public string Country { get; set; }

        [Display(Name = "Postal Code")]
        [StringLength(200, MinimumLength = 5)]
        [Required]
        public string PostalCode { get; set; }

    }
}