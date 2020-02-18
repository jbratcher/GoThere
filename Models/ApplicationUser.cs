using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace GoThere.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        [PersonalData]
        [Required]
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        [PersonalData]
        [Required]
        public string LastName { get; set; }

        [PersonalData]
        public string FullName = "{firstName} {lastName}";

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        [PersonalData]
        [Required]
        public string Occupation { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Text)]
        [PersonalData]
        [Required]
        public string Industry { get; set; }

        [Display(Name = "Postal Code")]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.PostalCode)]
        [PersonalData]
        [Required]
        public string PostalCode { get; set; }

    }
}
