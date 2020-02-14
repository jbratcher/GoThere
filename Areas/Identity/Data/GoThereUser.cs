using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GoThere.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the GoThereUser class
    public class GoThereUser : IdentityUser
    {
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }

        public string FullName = "{firstName} {lastName}";

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Occupation { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Industry { get; set; }

        [Display(Name = "Postal Code")]
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string PostalCode { get; set; }

    }
}
